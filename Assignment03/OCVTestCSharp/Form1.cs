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

         pictureBox1.Image = new Bitmap( @"..\..\Resources\Obama1.jpg" );
         pictureBox3.Image = new Bitmap( @"..\..\Resources\Chessboard.jpg" );

         Image<Bgr, byte> koImg1 = new Image<Bgr, byte >( ( Bitmap )pictureBox1.Image );
         Image<Bgr, byte> koImg2 = new Image<Bgr, byte >( ( Bitmap )pictureBox3.Image );
         Image<Bgr, byte> koImg4 = new Image<Bgr, byte >( koImg2.Size );
         UMat koIn1  = koImg1.ToUMat( );
         UMat koOut1 = new UMat( );

         CvInvoke.Canny       ( koIn1, koOut1, 150, 50 );
         CvInvoke.CornerHarris( koImg2, koImg4, 2 ); //, 0.0001, 255.0 );
         pictureBox2.Image = koOut1.Bitmap;
         pictureBox4.Image = koImg4.ToBitmap( );        
      }


   }
}
