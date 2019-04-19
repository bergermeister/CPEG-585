using System.Collections.Generic;
using System.IO;

using FaceRecogPCA;
using LDA.NEigen;

namespace LDA
{
   public class TcFaceRecog
   {
      private string                        voPath;
      private int                           viEc;     /**< Number of Eigen Values to keep */
      private int                           viN;      /**< Size of a sample */
      private Dictionary< string, TcClass > voClass;  /**< Dictionary of classes */
      private List< TcVector >              voEV;     /**< List of Eigen Vectors */ 
      private Matrix                        voSb;     /**< Between-class Scatter Matrix */
      private Matrix                        voSw;     /**< Within-class Scatter Matrix */
      private Matrix                        voMb;     /**< Overall Mean */

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

         /// -# Read in all training images
         this.mReadImages( );

         /// -# Initialize the scatter matrices and overall mean vector
         this.mInitMatrices( );

         /// -# Compute the mean vectors
         this.mComputeMeanVectors( );

         /// -# Compute the Eigen Values and Vectors
         this.mComputeEigenValues( );
      }

      private void mReadImages( )
      {
         FileInfo koInfo;
         string   koId = string.Empty;

         /// For each file in the path:
         foreach( string koFile in Directory.EnumerateFiles( this.voPath ) )
         {
            /// -# Obtain the file information
            koInfo = new FileInfo( koFile );

            /// -# Parse the first 3 characters of the file name as the class identifier
            koId = koInfo.Name.Substring( 0, 3 );

            /// -# If the class is not in the dictionary, create one and add it
            if( !this.voClass.ContainsKey( koId ) )
            {
               this.voClass.Add( koId, new TcClass( koId ) );
            }

            /// -# Add the file as a new sample image to the class
            this.voClass[ koId ].Add( new TcSampleImage( koInfo.FullName ) );
         }

         /// -# Record the length of a sample
         this.viN = voClass[ koId ][ 0 ].ViLength;
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
            voMb = ( Matrix )voMb.Addition( koC.MGetMean( ) );
         }

         /// -# Divide the overall mean vector by the number of classes
         this.voMb.Multiply( 1.0 / voClass.Count );
      }

      private void mComputeScatterMatrics( )
      {
         int    kiI;
         Matrix koSi; // Class Scatter Matrix
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

               /// -# Multiply the temporary Vector by the transpose of the temporary Vector to get the class Scatter Matrix
               koSi = ( Matrix )koSi.Multiply( koSi.Transpose( ) );

               /// -# Add the class Scatter Matrix to the Within-class Scatter Matrix
               this.voSw = ( Matrix )voSw.Addition( koSi );
            }
         }

         /// -# TODO: Take the Inverse here for later use
         this.voSw = ( Matrix )this.voSw.Inverse;

         /// -# For each class:
         foreach( TcClass koC in voClass.Values )
         {
            /// -# Get the Class Mean Vector and subtract the overall mean vector from it
            koMc = ( Matrix )koC.MGetMean( ).Subtraction( voMb );

            /// -# Multiply the adjusted Mean Vector by the Transpose of the adjusted Mean Vector, and
            ///    add it to the Between Class Scatter Matrix
            voSb = ( Matrix )voSb.Addition( koMc.Multiply( koMc.Transpose( ) ) ).Multiply( koMc.Rows );
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
         koRes = ( Matrix )voSw.Multiply( voSb );
         koEDecomp = koRes.GetEigenvalueDecomposition( );

         /// -# Obtain the Eigen Vector Matrix
         koEMat = koEDecomp.EigenvectorMatrix;

         /// -# Obtain the Real Eigen Values
         kdEVal = koEDecomp.RealEigenvalues;

         /// -# Create a new list of Eigen Vectors
         this.voEV = new List< TcVector >( );

         /// -# Create empty eigen vector
         kdEVec = new double[ voSb.Columns ];
         
         for( kiC = 0; kiC < voSb.Columns; kiC++ )
         {
            for( kiR = 0; kiR < voSb.Rows; kiR++ )
            {
               kdEVec[ kiR ] = koEMat[ kiR, kiC ];
            }
            this.voEV.Add( new TcVector( kdEVal[ kiC ], kdEVec, voSb.Columns ) );
         }

         this.voEV.Sort( );
      }

      private void mSelectLinearDiscriminants( )
      {

      }

      private void mTrasnformOntoSubspace( )
      {

      }
   }
}
