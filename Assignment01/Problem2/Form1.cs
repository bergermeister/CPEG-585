using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problem2
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();

         this.VoPBOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
      }

      private void VoBtnBrowse_Click( object aoSender, EventArgs aEe )
      {
         OpenFileDialog koDlg = new OpenFileDialog( );
         Bitmap         koBmp;

         koDlg .Filter = "Bitmap Files|*.bmp";
         koDlg.Title   = "Select a Bitmap File";

         // Show the Dialog
         if( koDlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
         {
            koBmp = new Bitmap( koDlg.FileName );
            this.VoPBOriginal.Image = koBmp;
         }
      }

      private void VoBtnHE_Click( object aoSender, EventArgs aoE ) 
      {
         HistogramEqualizer koHE;
         Bitmap             koBmp; 
         FormPicture        koForm;

         if( ( Bitmap )this.VoPBOriginal.Image != null )
         {
            koHE = new HistogramEqualizer( );
            koBmp = koHE.MProcess( ( Bitmap )this.VoPBOriginal.Image );
            koForm = new FormPicture( koBmp );
            koForm.Show( this );
         }
         else
         {
            MessageBox.Show( "Please browse for a bitmap to process." );
         }
      }
   }
}
