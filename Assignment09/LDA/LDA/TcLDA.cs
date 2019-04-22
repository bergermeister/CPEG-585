namespace LDA.LDA
{
   using System;
   using System.Collections.Generic;
   using FaceRecogPCA;
   using NEigen;

   public class TcLDA
   {
      private int                           viN;      /**< Size of a sample */
      private int                           viCount;  /**< Number of samples */
      private int                           viCountW; /**< Number of Projection Vectors to use */
      private List< TcImage >               voImages; /**< List of all samples */
      private Dictionary< string, TcClass > voClass;  /**< Dictionary of classes */
      private List< TcVector >              voEV;     /**< List of Eigen Vectors */ 
      private Matrix                        voSb;     /**< Between-class Scatter Matrix */
      private Matrix                        voSw;     /**< Within-class Scatter Matrix */
      private Matrix                        voMb;     /**< Overall Mean */
      private Matrix                        voW;      /**< Projection (Eigen Vector) Matrix */

      public TcLDA( List< TcImage > koImages, Dictionary< string, TcClass > koClasses, int kiSampleSize )
      {
         /// -# Store the images
         this.voImages = koImages;

         /// -# Store the class dictionary
         this.voClass = koClasses;

         /// -# Store the Sample Size
         this.viN = kiSampleSize;

         /// -# Initialize the scatter matrices and overall mean vector
         this.mInitMatrices( );

         /// -# Count the number of samples
         this.viCount = 0;
         foreach( TcClass koClass in this.voClass.Values )
         {
            this.viCount += koClass.Count;
         }

         this.viCountW = this.voClass.Count - 1;
      }

      public void MTrain( )
      {
         /// -# Compute the mean vectors
         this.mComputeMeanVectors( );

         /// -# Compute Scatter Matrices
         this.mComputeScatterMatrics( );

         /// -# Compute the Eigen Values and Vectors
         this.mComputeEigenValues( );

         /// -# Build Projection Matrix (W)
         this.mSelectLinearDiscriminants( );

         /// -# Project the samples
         this.mProject( );
      }

      public void MMatches( TcImage aoImg, ref TcMatch[ ] aoMatches )
      {
         TcMatch[ ] koMatches;
         double     kdDist = 0.0;
         int        kiI, kiJ;

         /// -# Project the image
         this.mProject( aoImg );

         /// -# Find the best match
         koMatches = new TcMatch[ this.voImages.Count ];
         for( kiI = 0; kiI < this.voImages.Count; kiI++ )
         {
            kdDist = 0;
            for( kiJ = 0; kiJ < this.viCountW; kiJ++ )
            {
               kdDist += ( ( this.voImages[ kiI ].VdVecLDA[ kiJ ] - aoImg.VdVecLDA[ kiJ ] ) *
                           ( this.voImages[ kiI ].VdVecLDA[ kiJ ] - aoImg.VdVecLDA[ kiJ ] ) );
            }
            kdDist = Math.Sqrt( kdDist );
            koMatches[ kiI ] = new TcMatch( this.voImages[ kiI ], kdDist, kiI );
         }

         Array.Sort( koMatches );
         aoMatches = koMatches;
      }

      private void mInitMatrices( )
      {
         /// -# Create an empty Within-Class Scatter Matrix (Sw)
         this.voSw = new Matrix( this.viN, this.viN );

         /// -# Create an empty Between-Class Scatter Matrix (Sb)
         this.voSb = new Matrix( this.viN, this.viN );

         /// -# Create an empty Overall Mean Vector
         this.voMb = new Matrix( this.viN, 1 );
      }

      private void mComputeMeanVectors( )
      {
         foreach( TcClass koC in voClass.Values )
         {
            /// -# Calculate the Mean Vector for each class
            koC.MCalculateMeanVector( );

            /// -# Add each Mean Vector to the overall Mean Vector
            this.voMb = ( Matrix )voMb.Addition( koC.MGetMean( ) );
         }

         /// -# Divide the overall mean vector by the number of classes
         this.voMb.Multiply( 1.0 / this.voClass.Count );
      }

      private void mComputeScatterMatrics( )
      {
         int    kiI;
         Matrix koSi; // Covariance Matrix of a class
         Matrix koMc; // Class Mean Vector

         /// For each class:
         foreach( TcClass koC in voClass.Values )
         {
            /// -# Obtain the Mean Vector
            koMc = koC.MGetMean( );

            for( kiI = 0; kiI < koC.Count; kiI++ )
            {
               /// -# Subtract the Class Mean Vector from each Sample and store it in a tempory Vector
               koSi = ( Matrix )koC.MGetSample( kiI ).Subtraction( koMc );

               /// -# Multiply the temporary Vector by the transpose of the temporary Vector to get the Covariance Matrix
               koSi = ( Matrix )koSi.Multiply( koSi.Transpose( ) );

               /// -# Add the class Scatter Matrix to the Within-class Scatter Matrix
               this.voSw = ( Matrix )voSw.Addition( koSi );
            }
         }

         /// -# For each class:
         foreach( TcClass koC in voClass.Values )
         {
            /// -# Get the Class Mean Vector and subtract the overall mean vector from it
            koMc = ( Matrix )koC.MGetMean( ).Subtraction( voMb );

            /// -# Multiply adjusted Mean Vector by the transpose of the adjusted Mean Vector
            koSi = ( Matrix )koMc.Multiply( koMc.Transpose( ) );
            
            /// -# Multiply by the number of samples in the class
            koSi = ( Matrix )koSi.Multiply( koC.Count );

            /// -# Multiply the adjusted Mean Vector by the Transpose of the adjusted Mean Vector, and
            ///    add it to the Between Class Scatter Matrix
            voSb = ( Matrix )voSb.Addition( koSi );
         }
      }

      private void mComputeEigenValues( )
      {
         IEigenvalueDecomposition koEDecomp;
         IMatrix                  koEMat;
         Matrix                   koRes;
         double[ ]                kdEVal;
         double[ ]                kdEVec;
         int                      kiR, kiC;

         /// -# Calculate the Eigen Decomposition for (Sw^-1)(Sb)
         koRes = ( Matrix )this.voSw.Inverse.Multiply( voSb );
         koEDecomp = koRes.GetEigenvalueDecomposition( );

         /// -# Obtain the Eigen Vector Matrix
         koEMat = koEDecomp.EigenvectorMatrix;

         /// -# Obtain the Real Eigen Values
         kdEVal = koEDecomp.RealEigenvalues;

         /// -# Create a new list of Eigen Vectors
         this.voEV = new List< TcVector >( );

         /// -# Create empty eigen vector
         kdEVec = new double[ this.viN ];
         
         /// -# Add the eigen vectors to the list of eigen vectors
         for( kiC = 0; kiC < this.viN; kiC++ )
         {
            for( kiR = 0; kiR < voSb.Rows; kiR++ )
            {
               kdEVec[ kiR ] = koEMat[ kiR, kiC ];
            }
            this.voEV.Add( new TcVector( kdEVal[ kiC ], kdEVec, this.viN ) );
         }

         // TODO: Check Eigen Values
         //for( kiC = 0; kiC < this.viN; kiC++ )
         //{
         //   Matrix koTemp = ( Matrix )koEMat.Submatrix( 0, this.viN - 1, kiC, kiC );
         //   Matrix koComp = ( Matrix )koRes.Multiply( koTemp );
         //   koTemp = ( Matrix )koTemp.Multiply( kdEVal[ kiC ] );
         //}

         /// -# Sort the Eigen Vector lsit
         this.voEV.Sort( );
      }

      private void mSelectLinearDiscriminants( )
      {
         int kiR, kiC;

         /// -# Create the Eigen Vector Matrix
         this.voW = new Matrix( this.viN, this.viCountW );
         for( kiC = 0; kiC < this.viCountW; kiC++ )
         {
            for( kiR = 0; kiR < this.voEV[ kiC ].VdData.Length; kiR++ )
            {
               this.voW[ kiR, kiC ] = this.voEV[ kiC ].VdData[ kiR ];
            }
         }
      }

      private void mProject( )
      {
         foreach( TcImage koImg in this.voImages )
         {
            this.mProject( koImg );
         }
      }

      private void mProject( TcImage aoImg )
      {
         Matrix koX = new Matrix( this.viN, 1 );
         Matrix koW = this.voW; 
         Matrix koY;
         int   kiR; 
         
         /// -# Convert Image Data into Matrix
         for( kiR = 0; kiR < this.viN; kiR++ )
         {
            koX[ kiR, 0 ] = aoImg.VdVecRdc[ kiR ];
         }
         
         /// -# Project Image: Y = W.T * X
         koY = ( Matrix )koW.Transpose( ).Multiply( koX );
         
         /// -# Store the projection in the image
         aoImg.VdVecLDA = new double[ koY.Rows ];
         for( kiR = 0; kiR < koY.Rows; kiR++ )
         {
            aoImg.VdVecLDA[ kiR ] = koY[ kiR, 0 ];
         }
      }
   }
}
