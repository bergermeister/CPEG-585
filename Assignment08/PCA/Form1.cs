using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using PCALib;

namespace PCA
{
   public partial class Form1 : Form
   {
      private List< Bitmap > voImage;
      private List< Matrix > voVector;
      private Matrix         voMean;
      private Matrix         voMatAdj;
      private Matrix         voMatCov;
      private Matrix         voMatEig;
      private Matrix         voBasVec;
      private List< Matrix > voSample;

      public Form1()
      {
         InitializeComponent();
      }

      private List< Bitmap > mReadImages( string aoPath )
      {
         List< Bitmap > koImg = new List< Bitmap >( );

         // For each file in the given directory
         foreach( string koFile in Directory.EnumerateFiles( aoPath ) )
         {
            koImg.Add( new Bitmap( koFile ) );
         }

         return( koImg );
      }

      private Matrix mVectorizeImage( Bitmap aoBmp )
      {
         Matrix koMat = new Matrix( aoBmp.Width * aoBmp.Height, 1 );
         Color  koC;
         int    kiX, kiY, kiI;

         kiI = 0;
         for( kiY = 0; kiY < aoBmp.Height; kiY++ )
         { 
            for( kiX = 0; kiX < aoBmp.Width; kiX++ )
            {
               // Obtain the pixel color
               koC = aoBmp.GetPixel( kiX, kiY );

               // Store the pixel color as grayscale
               koMat[ kiI, 0 ] = ( ( 0.299 * koC.R ) + ( 0.587 * koC.G ) + ( 0.114 * koC.B ) );

               kiI++;
            }
         }
       
         return( koMat );
      }

      private Matrix mFindMeanVector( List< Matrix > aoSample )
      {
         Matrix koMean = new Matrix( aoSample[ 0 ].Rows, aoSample[ 0 ].Columns );
         int    kiRow;

         foreach( Matrix koMat in this.voVector )
         {
            for( kiRow = 0; kiRow < koMat.Rows; kiRow++ )
            {
               koMean[ kiRow, 0 ] += ( koMat[ kiRow, 0 ] / this.voVector.Count );
            }
         }

         return( koMean );
      }

      private Matrix mMeanAdjusted( List< Matrix > aoSample, Matrix aoMean )
      {
         Matrix koAdj = new Matrix( aoSample[ 0 ].Rows, aoSample.Count );
         int    kiCol, kiRow;

         for( kiCol = 0; kiCol < aoSample.Count; kiCol++ )
         {
            for( kiRow = 0; kiRow < aoSample[ kiCol ].Rows; kiRow++ )
            {
               koAdj[ kiRow, kiCol ] = aoSample[ kiCol ][ kiRow, 0 ] - aoMean[ kiRow, 0 ];
            }
         }

         return( koAdj );
      }
  
      private Matrix mComputeEigenMatrix( Matrix aoCov, int aiFaces )
      {
         IEigenvalueDecomposition               koEigenDecomp;
         List< KeyValuePair< double, Matrix > > koEigVec;
         double                                 kdVal;
         Matrix                                 koMat;
         Matrix                                 koMatEig;
         int                                    kiRow, kiCol;

         koEigenDecomp = aoCov.GetEigenvalueDecomposition( );
         koEigVec = new List<KeyValuePair< double, Matrix > >( );
         for( kiCol = 0; kiCol < koEigenDecomp.RealEigenvalues.Length; kiCol++ )
         {
            kdVal = koEigenDecomp.RealEigenvalues[ kiCol ];

            if( kdVal < 0 )
            {
               kdVal = -kdVal;
            }

            koMatEig = ( Matrix )koEigenDecomp.EigenvectorMatrix;
            koMat    = ( Matrix )koMatEig.Submatrix( 0, koMatEig.Rows - 1, kiCol, kiCol );
            koEigVec.Add( new KeyValuePair< double, Matrix >( kdVal, koMat ) );
         }

         koEigVec.Sort( new KeyValuePairComparer( ) );
         koMat = new Matrix( koEigenDecomp.EigenvectorMatrix.Rows, aiFaces );
         for( kiCol = 0; kiCol < aiFaces; kiCol++ )
         {
            for( kiRow = 0; kiRow < koMat.Rows; kiRow++ )
            {
               koMat[ kiRow, kiCol ] = koEigVec[ koEigVec.Count - kiCol - 1 ].Value[ kiRow, 0 ];
            }
         }

         return( koMat );
      }

      private double mEuclideanDistance( Matrix aoVec1, Matrix aoVec2 )
      {
         double kdDist = 0.0;
         int    kiCol;

         for( kiCol = 0; kiCol < aoVec1.Columns; kiCol++ )
         {
            kdDist += ( aoVec1[ 0, kiCol ] - aoVec2[ 0, kiCol ] ) * ( aoVec1[ 0, kiCol ] - aoVec2[ 0, kiCol ] );
         }

         kdDist = Math.Sqrt( kdDist );

         return( kdDist );
      }

      public class KeyValuePairComparer : IComparer< KeyValuePair< double, Matrix > >
      {
         public int Compare( KeyValuePair< double, Matrix > aoA, KeyValuePair< double, Matrix > aoB )
         {
            int kiStatus = 1;

            if( aoA.Key == aoB.Key )
            {
               kiStatus = 0;
            }
            else if( aoA.Key < aoB.Key )
            {
               kiStatus = -1;
            }

            return( kiStatus );
         }
      }

      private void button1_Click(object sender, EventArgs e)
      {
         OpenFileDialog koDlg = new OpenFileDialog( );
         Bitmap         koBmp;

         //txt files (*.txt)|*.txt|All files (*.*)|*.*
         koDlg .Filter = "JPEG|*.jpg|Bitmap Files|*.bmp";
         koDlg.Title   = "Select an Image File";

         // Show the Dialog
         if( koDlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
         {
            koBmp = new Bitmap( koDlg.FileName );
            this.pictureBox1.Image = koBmp;
         }
      }

      private void button2_Click(object sender, EventArgs e)
      {
         Bitmap koBmp = ( Bitmap )this.pictureBox1.Image;
         Matrix koMat;
         Matrix koEig;
         Matrix koSub;
         int    kiCol;
         double kdDistMin = Double.MaxValue;
         double kdDistCur;
         int    kiIdx = -1;

         // Create a new matrix to store the vectorized data
         koMat = this.mVectorizeImage( koBmp );

         for( kiCol = 0; kiCol < this.voVector.Count; kiCol++ )
         {
            // koSub = ( Matrix )koMat.Subtraction( this.voMean ); 
            koSub = ( Matrix )this.voMatAdj.Submatrix( 0, this.voMatAdj.Rows - 1, kiCol, kiCol );
            koEig = ( Matrix )koMat.Subtraction( koSub ).Transpose( ).Multiply( this.voBasVec );
            kdDistCur = this.mEuclideanDistance( koEig, this.voSample[ kiCol ] );
            if( kdDistCur < kdDistMin )
            {
               kdDistMin = kdDistCur;
               kiIdx     = kiCol;
            }
         }

         this.pictureBox2.Image = this.voImage[ kiIdx ];
         this.textBox2.Text     = kdDistMin.ToString( );
      }

      private void button3_Click(object sender, EventArgs e)
      {
         int    kiFaces;
         int    kiCol;
         Matrix koMat;
         Matrix koSub;

         if( Int32.TryParse( this.textBox1.Text.ToString( ), out kiFaces ) )
         {  
            // Create a new dictionary of training images for each face
            this.voImage = this.mReadImages( @"..\..\Resources\ATTFaceDataSet\Training" );

            // Vectorize the imgages
            this.voVector = new List< Matrix >( ); 
            foreach( Bitmap koBmp in this.voImage )
            {
               this.voVector.Add( this.mVectorizeImage( koBmp ) );
            }

            // Find the Mean Vector
            this.voMean = this.mFindMeanVector( this.voVector );

            // Calculate the Mean Adjusted Vectors
            this.voMatAdj = this.mMeanAdjusted( this.voVector, this.voMean );

            // Calculate Covariant Matrix
            this.voMatCov = ( Matrix )this.voMatAdj.Transpose( ).Multiply( this.voMatAdj );

            // Calculate Eigen Matrix
            this.voMatEig = this.mComputeEigenMatrix( this.voMatCov, kiFaces );

            // Calculate Base Vectors
            this.voBasVec = ( Matrix )( this.voMatAdj.Multiply( this.voMatEig ) );

            // Calculate samples
            this.voSample = new List< Matrix >( );
            for( kiCol = 0; kiCol < this.voVector.Count; kiCol++ )
            {
               // koSub = ( Matrix )this.voVector[ kiCol ].Subtraction( this.voMean ); 
               koSub = ( Matrix )this.voMatAdj.Submatrix( 0, this.voMatAdj.Rows - 1, kiCol, kiCol );
               koMat = ( Matrix )this.voVector[ kiCol ].Subtraction( koSub ).Transpose( ).Multiply( this.voBasVec );
               this.voSample.Add( koMat );
            }
         }
         else
         {
            MessageBox.Show( "Please enter a number" );
         }
      }
   }
}
