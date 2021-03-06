﻿namespace LDA
{
   using System;
   using System.Drawing;
   using System.IO;
   using System.Linq;
   using System.Windows.Forms;

   public partial class Form1 : Form
   {
      private const string voPath = @"..\..\..\..\ATTFaceDataSet\";
      private TcFaceRecog  voFR;

      public Form1()
      {
         InitializeComponent();
      }

      private void voBtnTrain_Click(object sender, EventArgs e)
      {
         int kiW, kiH;

         this.voFR = new TcFaceRecog( voPath + "Training" );
         this.voFR.MTrain( );

         /// -# Show the Average Image
         this.voImgAvg.Image = this.voFR.VoPCA.VoImgAvg.VoBmp;

         /// -# Show top five eigen faces
         kiW = this.voFR.VoPCA.VoImgAvg.ViWidth;
         kiH = this.voFR.VoPCA.VoImgAvg.ViHeight;
         this.mNormalizeDataAndShowFace( ( double[ ] )this.voFR.VoPCA.VoEF[ 0 ].VdData.Clone( ), kiW, kiH, this.voImgEF0 );
         this.mNormalizeDataAndShowFace( ( double[ ] )this.voFR.VoPCA.VoEF[ 1 ].VdData.Clone( ), kiW, kiH, this.voImgEF1 );
         this.mNormalizeDataAndShowFace( ( double[ ] )this.voFR.VoPCA.VoEF[ 2 ].VdData.Clone( ), kiW, kiH, this.voImgEF2 );
         this.mNormalizeDataAndShowFace( ( double[ ] )this.voFR.VoPCA.VoEF[ 3 ].VdData.Clone( ), kiW, kiH, this.voImgEF3 );
         this.mNormalizeDataAndShowFace( ( double[ ] )this.voFR.VoPCA.VoEF[ 4 ].VdData.Clone( ), kiW, kiH, this.voImgEF4 );
      }

      private void voBtnTestImg_Click(object sender, EventArgs e)
      {
         OpenFileDialog koDlg = new OpenFileDialog( );
         TcMatch[ ]     koMatchesPCA = new TcMatch[ this.voFR.VoImages.Count ];
         TcMatch[ ]     koMatchesLDA = new TcMatch[ this.voFR.VoClasses.Count ];
         TcImage        koImg;
         TcImage        koRec;

         //txt files (*.txt)|*.txt|All files (*.*)|*.*
         koDlg .Filter = "JPEG|*.jpg|Bitmap Files|*.bmp";
         koDlg.Title   = "Select an Image File";

         // Show the Dialog
         if( koDlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
         {
            koImg = new TcImage( koDlg.FileName );
            this.mNormalizeDataAndShowFace( ( double[ ] )koImg.VdVec.Clone( ), koImg.ViWidth, koImg.ViHeight, this.voImgChk );

            this.voFR.VoPCA.MAdjustImage( koImg );
            this.mNormalizeDataAndShowFace( ( double[ ] )koImg.VdVecAdj.Clone( ), koImg.ViWidth, koImg.ViHeight, this.voImgAdj );

            koRec = this.voFR.MReconstruct( koImg, ref koMatchesPCA, ref koMatchesLDA );
            this.mNormalizeDataAndShowFace( ( double[ ] )koRec.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgRec );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesLDA[ 0 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch0 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesLDA[ 1 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch1 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesLDA[ 2 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch2 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesLDA[ 3 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch3 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesPCA[ 0 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch4 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesPCA[ 1 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch5 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesPCA[ 2 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch6 );
            this.mNormalizeDataAndShowFace( ( double[ ] )koMatchesPCA[ 3 ].VoImg.VdVec.Clone( ), koRec.ViWidth, koRec.ViHeight, this.voImgMatch7 );
         }
      }

      private void voBtnCompAcc_Click(object sender, EventArgs e)
      {
         TcMatch[ ] koMatchesPCA = new TcMatch[ this.voFR.VoImages.Count ];
         TcMatch[ ] koMatchesLDA = new TcMatch[ this.voFR.VoClasses.Count ];
         TcImage    koImg;
         TcImage    koRec;
         double     kdAccPCA = 0.0;
         double     kdAccLDA = 0.0;
         double     kdCount  = 0.0;

         /// -# Read all images into the list
         foreach( string koFile in Directory.EnumerateFiles( voPath + @"Testing\" ) )
         {
            koImg = new TcImage( koFile );
            koRec = this.voFR.MReconstruct( koImg, ref koMatchesPCA, ref koMatchesLDA );
            if( koMatchesPCA[ 0 ].VoImgName.Substring( 0, 3 ) == koImg.VoName.Substring( 0, 3 ) )
            {
               kdAccPCA += 1.0;
            }

            if( koMatchesLDA[ 0 ].VoImgName.Substring( 0, 3 ) == koImg.VoName.Substring( 0, 3 ) )
            {
               kdAccLDA += 1.0;
            }
            else
            {
               Console.WriteLine( koImg.VoName + "\t" + koMatchesLDA[ 0 ].VoImgName );
            }

            kdCount += 1.0;
         }

         kdAccPCA /= kdCount;
         kdAccPCA *= 100.0;

         kdAccLDA /= kdCount;
         kdAccLDA *= 100.0;

         MessageBox.Show( "PCA Accuracy = " + kdAccPCA + "%" + Environment.NewLine + "LDA Accuracy = " + kdAccLDA + "%" );
      }

      private void mNormalizeDataAndShowFace( double[ ] adData, int aiWidth, int aiHeight, PictureBox aoPB )
      {
         double kdMax = ( from kdN in adData select kdN ).Max( );
         double kdMin = ( from kdN in adData select kdN ).Min( );
         double kdDlt = kdMax - kdMin;
         int    kiR, kiC, kiI;
         Bitmap koBmp = new Bitmap( aiWidth, aiHeight );

         for( kiI = 0; kiI < ( aiWidth * aiHeight ); kiI++ )
         {
            adData[ kiI ] = adData[ kiI ] - kdMin;
            adData[ kiI ] = ( adData[ kiI ] / kdDlt ) * 255.0;
            if( adData[ kiI ] < 0 )
            {
               adData[ kiI ] = 0;
            }
            if( adData[ kiI ] > 255.0 )
            {
               adData[ kiI ] = 255.0;
            }
         }

         kiI = 0;
         for( kiR = 0; kiR < aiHeight; kiR++ )
         {
            for( kiC = 0; kiC < aiWidth; kiC++ )
            {
               koBmp.SetPixel( kiC, kiR, Color.FromArgb( ( int )adData[ kiI ],
                                                         ( int )adData[ kiI ],
                                                         ( int )adData[ kiI ] ) );
               kiI++;
            }
         }
         aoPB.Image = koBmp;
      }
   }
}
