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
   public partial class FormPicture : Form
   {
      private Bitmap voBmp;

      public FormPicture( Bitmap aoBmp )
      {
         this.voBmp = aoBmp;

         InitializeComponent();
      }

      private void FormPicture_Load(object sender, EventArgs e)
      {
         this.VoPB.SizeMode = PictureBoxSizeMode.StretchImage;
         this.VoPB.Image = this.voBmp;
      }
   }
}
