using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Mapack;

namespace PCA
{
   public partial class Form1 : Form
   {
      private Dictionary< string, List< Bitmap > > voImage;
      private Dictionary< string, List< Matrix > > voVector;
      private Dictionary< string, Matrix >         voMean;
      private Dictionary< string, Matrix >         voMatAdj;
      private Dictionary< string, Matrix >         voMatCov;

      public Form1()
      {
         InitializeComponent();

         this.mReadImages( @"Resources\ATTFaceDataSet\Training" );
         this.mVectorizeImages( );
         this.mFindMeanVector( );
         this.mMeanAdjusted( );
         this.mComputeCovariance( );
      }

      private void mReadImages( string aoPath )
      {
         string koId;

         // Create a new dictionary of training images for each face
         this.voImage = new Dictionary< string, List< Bitmap > >( );

         // For each file in the given directory
         foreach( string koFile in Directory.EnumerateFiles( aoPath ) )
         {
            // Get the Face's ID (Everything before the '_')
            koId = koFile.Split( '_' )[ 0 ];

            // Add the face ID if it does not exist
            if( !this.voImage.ContainsKey( koId ) )
            {
               this.voImage.Add( koId, new List< Bitmap >( ) );
            }

            // Add the image to the training dictionary
            this.voImage[ koId ].Add( new Bitmap( koFile ) );
         }
      }

      private void mVectorizeImages( )
      {
         Matrix koMat;
         Color  koC;
         int    kiX, kiY, kiI;

         // Create a new dictionary of vectorized training data
         this.voVector = new Dictionary< string, List< Matrix > >( );

         // For each face
         foreach( KeyValuePair< string, List< Bitmap > > koFace in this.voImage )
         {
            // If the vectorized dictionary does not contain the face
            if( !this.voVector.ContainsKey( koFace.Key ) )
            {
               this.voVector.Add( koFace.Key, new List< Matrix >( ) );
            }

            // For each training image
            foreach( Bitmap koBmp in koFace.Value )
            {
               // Create a new matrix to store the vectorized data
               koMat = new Matrix( koBmp.Width * koBmp.Height, 1 );

               kiI = 0;
               for( kiY = 0; kiY < koBmp.Height; kiY++ )
               { 
                  for( kiX = 0; kiX < koBmp.Width; kiX++ )
                  {
                     // Obtain the pixel color
                     koC = koBmp.GetPixel( kiX, kiY );

                     // Store the pixel color as grayscale
                     koMat[ kiI, 0 ] = ( ( 0.299 * koC.R ) + ( 0.587 * koC.G ) + ( 0.114 * koC.B ) );
                  }
               }

               // Store the Matrix
               this.voVector[ koFace.Key ].Add( koMat );
            }
         }
      }

      private void mFindMeanVector()
      {
         int kiI;
         Matrix koMean;

         this.voMean = new Dictionary< string, Matrix >( );

         foreach( KeyValuePair< string, List< Matrix > > koFace in this.voVector )
         {
            if( !this.voMean.ContainsKey( koFace.Key ) )
            {
               this.voMean.Add( koFace.Key, new Matrix( koFace.Value[ 0 ].Rows, 1 ) );
            }  

            koMean = this.voMean[ koFace.Key ];

            foreach( Matrix koVector in koFace.Value )
            {
               for( kiI = 0; kiI < koVector.Rows; kiI++ )
               {
                  koMean[ kiI, 0 ] += ( koVector[ kiI, 0 ] / koFace.Value.Count );
               }
            }
         }
      }

      private void mMeanAdjusted( )
      {
         Matrix koMean;
         Matrix koAdjust;
         int    kiCol, kiRow;

         this.voMatAdj = new Dictionary< string, Matrix >( );

         foreach( KeyValuePair< string, List< Matrix > > koFace in this.voVector )
         {
            if( !this.voMatAdj.ContainsKey( koFace.Key ) )
            {
               this.voMatAdj.Add( koFace.Key, new Matrix( koFace.Value[ 0 ].Rows, koFace.Value.Count ) );
            }

            koMean   = this.voMean[ koFace.Key ];
            koAdjust = this.voMatAdj[ koFace.Key ];
            for( kiCol = 0; kiCol < koFace.Value.Count; kiCol++ )
            {
               for( kiRow = 0; kiRow < koFace.Value[ kiCol ].Rows; kiRow++ )
               {
                  koAdjust[ kiRow, kiCol ] = koFace.Value[ kiCol ][ kiRow, 0 ] - koMean[ kiRow, 0 ];
               }
            }
         }
      }

      private void mComputeCovariance( )
      {
         Matrix koMatCov;
         Matrix koSi, koSj;
         int    kiI, kiJ;

         this.voMatCov = new Dictionary< string, Matrix >( );

         foreach( KeyValuePair< string, Matrix > koFace in this.voMatAdj )
         {
            koMatCov = this.voMatAdj[ koFace.Key ].Transpose( ) * this.voMatAdj[ koFace.Key ];

            if( !this.voMatCov.ContainsKey( koFace.Key ) )
            {
               this.voMatCov.Add( koFace.Key, koMatCov );
            }
         }
      }
   }
}
