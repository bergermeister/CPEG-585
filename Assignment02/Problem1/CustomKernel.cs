using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problem1
{
   public partial class CustomKernel : Form
   {
      private TextBox[ ][ ] vopM;

      public double[ ][ ] VopM{ get; set; }

      public CustomKernel()
      {
         int kiRow, kiCol;

         InitializeComponent();

         this.vopM = new TextBox[ 3 ][ ];
         this.VopM = new double[ 3 ][ ];
         for( kiRow = 0; kiRow < 3; kiRow++ )
         {
            this.vopM[ kiRow ] = new TextBox[ 3 ];
            this.VopM[ kiRow ] = new double[ 3 ];
         }
         
         this.vopM[ 0 ][ 0 ] = this.voTB11;
         this.vopM[ 0 ][ 1 ] = this.voTB12;
         this.vopM[ 0 ][ 2 ] = this.voTB13;

         this.vopM[ 1 ][ 0 ] = this.voTB21;
         this.vopM[ 1 ][ 1 ] = this.voTB22;
         this.vopM[ 1 ][ 2 ] = this.voTB23;

         this.vopM[ 2 ][ 0 ] = this.voTB31;
         this.vopM[ 2 ][ 1 ] = this.voTB32;
         this.vopM[ 2 ][ 2 ] = this.voTB33;

         for( kiRow = 0; kiRow < 3; kiRow++ )
         {
            for( kiCol = 0; kiCol < 3; kiCol++ )
            {
               this.vopM[ kiRow ][ kiCol ].Text = "0";
               this.VopM[ kiRow ][ kiCol ]      = 0;
            }
         }

         this.vopM[ 1 ][ 1 ].Text = "1";
         this.VopM[ 1 ][ 1 ]      = 1;
      }

      private void voBtnAccept_Click(object sender, EventArgs e)
      {
         int kiRow, kiCol;
         bool kbValid = true;

         for( kiRow = 0; kiRow < 3; kiRow++ )
         {
            for( kiCol = 0; kiCol < 3; kiCol++ )
            {
               kbValid = kbValid && Double.TryParse( this.vopM[ kiRow ][ kiCol ].Text, out this.VopM[ kiRow ][ kiCol ] );
            }
         }

         if( kbValid )
         { 
            this.Close( );
         }
         else
         {
            MessageBox.Show( "Invalid Kernel" );
         }
      }
   }
}
