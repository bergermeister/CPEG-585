using System.Collections.Generic;
using System.IO;

using FaceRecogPCA;
using LDA.NEigen;

namespace LDA
{
   public class TcFaceRecog
   {
      private string                        voPath;
      private PCA.TcPCA                     voPCA;


      private int                           viEc;     /**< Number of Eigen Values to keep */
      private int                           viN;      /**< Size of a sample */
      private Dictionary< string, TcClass > voClass;  /**< Dictionary of classes */
      private List< TcVector >              voEV;     /**< List of Eigen Vectors */ 
      private Matrix                        voSb;     /**< Between-class Scatter Matrix */
      private Matrix                        voSw;     /**< Within-class Scatter Matrix */
      private Matrix                        voMb;     /**< Overall Mean */
      private Matrix                        voW;      /**< Projection (Eigen Vector) Matrix */
      private Matrix                        voY;      /**< Projected Vectors */

      public TcFaceRecog( string aoPath, int kiEc )
      {
         /// -# Record the path
         this.voPath = aoPath;

         /// -# Record the number of Eigen Values to use
         this.viEc = kiEc;

         /// -# Initialize the dictionary of classes
         this.voClass = new Dictionary< string, TcClass >( );
      }

      public void MTrain( )
      {
         /// -# Empty the dictionary of classes
         this.voClass.Clear( );

         /// -# Run PCA to reduce dimensionality
         this.voPCA = new PCA.TcPCA( this.voPath, 40 );
         this.voPCA.MTrain( );

         /// -# Read in all training images from PCA
         this.mReadImages( );

         /// -# Initialize the scatter matrices and overall mean vector
         this.mInitMatrices( );

         /// -# Compute the mean vectors
         this.mComputeMeanVectors( );

         /// -# Compute Scatter Matrices
         this.mComputeScatterMatrics( );

         /// -# Compute the Eigen Values and Vectors
         this.mComputeEigenValues( );

         /// -# Build Projection Matrix (W)
         this.mSelectLinearDiscriminants( );
      }

      private void mReadImages( )
      {
         /// For each PCA Image:
         foreach( PCA.TcImage koImg in this.voPCA.VoImages )
         {
            /// -# If the Image is part of a new class, create a new class
            if( !this.voClass.ContainsKey( koImg.VoId ) )
            {
               this.voClass.Add( koImg.VoId, new TcClass( koImg.VoId ) );
            }

            /// -# Add the PCA Image's Projection Onto Reduced Dimension Vector as a sample
            this.voClass[ koImg.VoId ].Add( new TcSample( koImg.VdVecFSV ) );

            /// -# Record the length of a sample
            this.viN = koImg.VdVecFSV.Length;
         }

         /// -# Ec = Number of Classes - 1
         this.viEc = voClass.Count - 1;
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
         this.voMb.Multiply( 1.0 / voClass.Count );
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

            /// -# Multiply the adjusted Mean Vector by the Transpose of the adjusted Mean Vector, and
            ///    add it to the Between Class Scatter Matrix
            voSb = ( Matrix )voSb.Addition( koMc.Multiply( koMc.Transpose( ) ) ).Multiply( koC.Count );
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
         kdEVec = new double[ voSb.Columns ];
         
         /// -# Add the eigen vectors to the list of eigen vectors
         for( kiC = 0; kiC < voSb.Columns; kiC++ )
         {
            for( kiR = 0; kiR < voSb.Rows; kiR++ )
            {
               kdEVec[ kiR ] = koEMat[ kiR, kiC ];
            }
            this.voEV.Add( new TcVector( kdEVal[ kiC ], kdEVec, voSb.Columns ) );
         }

         /// -# Sort the Eigen Vector lsit
         this.voEV.Sort( );
      }

      private void mSelectLinearDiscriminants( )
      {
         int kiR, kiC;

         /// -# Create the Eigen Vector Matrix
         this.voW = new Matrix( this.voSb.Rows, this.viEc );
         for( kiC = 0; kiC < this.viEc; kiC++ )
         {
            for( kiR = 0; kiR < this.voEV[ kiC ].VdData.Length; kiR++ )
            {
               this.voW[ kiR, kiC ] = this.voEV[ kiC ].VdData[ kiR ];
            }
         }
      }

      private void mProject( )
      {
         ///Matrix koX = new Matrix(  
         ///Y = W.T * X
      }
   }
}
