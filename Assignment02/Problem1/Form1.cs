using System;
using System.Drawing;
using System.Windows.Forms;

namespace Problem1
{
   public partial class Form1 : Form
   {
      private const int xiSize = 5;
      private TcKernel voKernel;

      public Form1( )
      {
         InitializeComponent( );
         this.voRBtnIdentity.Checked = true;
      }

      private void mBtnConvolve_Click( object aoSender, EventArgs aoE )
      {
         if( this.voPicLeft.Image != null )
         { 
            try
            {                  
               this.voPicRight.Image = this.voKernel.MConvolve( ( Bitmap )this.voPicLeft.Image );
            }
            catch( Exception aoEx )
            {
               MessageBox.Show( aoEx.Message );
            }
         }
         else
         {
            MessageBox.Show( "Please select an image." );
         }
      }

      private void mBtnBrowse_Click( object aoSender, EventArgs aoE )
      {
         OpenFileDialog koDlg = new OpenFileDialog( );
         Bitmap         koBmp;

         koDlg.Filter = "Bitmap Files|*.bmp";
         koDlg.Title  = "Select a Bitmap File";

         // Show the Dialog
         if( koDlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
         {
            koBmp = new Bitmap( koDlg.FileName );
            this.voPicLeft.Image = koBmp;
         }
      }

      private void mKernelCheckedChanged( object aoSender, EventArgs aoE )
      {
         RadioButton koRBtn = aoSender as RadioButton;

         switch( koRBtn.Name )
         {
         case "voRBtnIdentity":
            this.voKernel = new TcKernelIdentity( xiSize );
            break;
         case "voRBtnAverage":
            this.voKernel = new TcKernelLowPass( xiSize );
            break;
         case "voRBtnHighPass":
            this.voKernel = new TcKernelHighPass( xiSize );
            break;
         case "voRBtnSharpen":
            this.voKernel = new TcKernelSharpen( xiSize, 0.5 );
            break;
         case "voRBtnGaussian":
            this.voKernel = new TcKernelGaussian( xiSize, 0.5 );
            break;
         case "voRBtnGradient":
            this.voKernel = new TcKernelGradient( xiSize );
            break;
         case "voRBtnLaplacian":
            this.voKernel = new TcKernelLaplacian( xiSize );
            break;
         case "voRBtnDiffGaussian":
            this.voKernel = new TcKernelGaussianDiff( xiSize, 0.5, 2.5 );
            break;
         }
         
      }
   }
}
