using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace OCVTestCSharp
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();    
      }

      private void button1_Click(object sender, EventArgs e)
      {
         Bitmap              koBmp = new Bitmap( @"..\..\Resources\Obama1.jpg" );
         Image< Gray, byte > koImg = new Image< Gray, byte >( koBmp );
         Image< Gray, byte > koCny = new Image< Gray, byte >( koBmp.Size );
         double kdCannyThresh     = 120.0;
         double kdCannyThreshLink = 120.0;

         // Perform Canny Edge Detection
         CvInvoke.Canny( koImg, koCny, kdCannyThresh, kdCannyThreshLink );

         // Display Original
         this.pictureBox1.Image = koImg.ToBitmap( );

         // Display Canny Edge
         this.pictureBox2.Image = koCny.ToBitmap( );
      }

      private void button2_Click(object sender, EventArgs e)
      {
         Bitmap               koBmp = new Bitmap( @"..\..\Resources\Chessboard.jpg" );
         Image< Gray, byte  > koImg = new Image< Gray, byte  >( koBmp );
         Image< Gray, float > koHrs = new Image< Gray, float >( koBmp.Size );
         Image< Gray, float > koInv = new Image< Gray, float >( koBmp.Size );
         Bitmap               koOut;
         Bitmap               koRes = new Bitmap( koBmp );
         Color                koC1;

         // Perform Harris Edge Detection
         CvInvoke.CornerHarris( koImg, koHrs, 2, 3, 0.04 );

         // Create threshold image
         CvInvoke.Threshold( koHrs, koInv, 0.0001, 255.0, ThresholdType.BinaryInv );

         // Mark the corners from the original Image as red
         koOut = koInv.ToBitmap( );
         for( int kiY = 0 ; kiY < koBmp.Height; kiY++ )
         {
            for( int kiX = 0; kiX < koBmp.Width; kiX++ )
            {
               koC1 = koOut.GetPixel( kiX, kiY );
               if( koC1.R == 0 )
               {
                  koRes.SetPixel( kiX, kiY, Color.FromArgb( 255, 0, 0 ) );
               }
            }
         }

         // Display Original
         this.pictureBox1.Image = koBmp; // koImg.ToBitmap( );

         // Display Harris Corner Detection
         this.pictureBox2.Image = koRes; // koInv.ToBitmap( );
      }
   }
}
