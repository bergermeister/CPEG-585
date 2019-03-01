using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PCALib;

namespace ImageRegistration
{
   public partial class Form1 : Form
   {
      private List< Point > voShape1;
      private List< Point > voShape2;

      public Form1( )
      {
         InitializeComponent();

         this.voShape1 = new List< Point >( );
         this.voShape2 = new List< Point >( );
      }

      /**
       * @brief Initialize Shapes
       */ 
      private void button1_Click(object sender, EventArgs e)
      {
         Transformation koT  = new Transformation( );
         Pen            koP1 = new Pen( Brushes.Blue, 1 );
         Pen            koP2 = new Pen( Brushes.Red,  1 );
         Graphics       koG  = this.panel1.CreateGraphics( );

         // Clear the lists of points
         this.voShape1.Clear( );
         this.voShape2.Clear( );

         // Initialize Shape1's list of points
         this.voShape1.Add( new Point(  20,  30 ) );
         this.voShape1.Add( new Point( 120,  50 ) );
         this.voShape1.Add( new Point( 160,  80 ) );
         this.voShape1.Add( new Point( 180, 300 ) );
         this.voShape1.Add( new Point( 100, 220 ) );
         this.voShape1.Add( new Point(  50, 280 ) );
         this.voShape1.Add( new Point(  20, 140 ) );

         koT.VdA = 1.05; koT.VdB = 0.05; koT.VdT1 = 15.0; koT.VdT2 = 22.0;
         this.voShape2 = this.mApplyTransformation( koT, this.voShape1 );
         this.voShape2[ 2 ] = new Point( this.voShape2[ 2 ].X + 10, this.voShape2[ 2 ].Y + 3 ); // Change one

         this.mDisplayShape( this.voShape1, koP1, koG );
         this.mDisplayShape( this.voShape2, koP2, koG );
      }

      private void button2_Click(object sender, EventArgs e)
      {
         Transformation koT  = new Transformation( );
         
         List< Point >  koS3;
         Pen            koPen1 = new Pen( Brushes.Blue, 1 );
         Pen            koPen2 = new Pen( Brushes.Red,  1 );
         Graphics       koG = this.panel2.CreateGraphics( );

         // Calculate A, B, T1, and T2
         Point  koP1;
         Point  koP2;
         Matrix koCi;
         Matrix koC = new Matrix( 4, 4 );
         Matrix koR = new Matrix( 4, 1 );
         Matrix koM;

         for( int kiRow = 0; kiRow < 4; kiRow++ )
         {
            for( int kiCol = 0; kiCol < 4; kiCol++ )
            {
               koC[ kiRow, kiCol ] = 0.0;
            }
            koR[ kiRow, 0 ] = 0.0;
         }

         for( int kiIdx = 0; kiIdx < this.voShape1.Count; kiIdx++ )
         {
            koP1 = this.voShape1[ kiIdx ];
            koP2 = this.voShape2[ kiIdx ];
            koC[ 0, 0 ] += ( 2 * koP2.X * koP2.X ) + ( 2 * koP2.Y * koP2.Y );
            koC[ 0, 1 ] += 0.0;
            koC[ 0, 2 ] += 2 * koP2.X;
            koC[ 0, 3 ] += 2 * koP2.Y;

            koC[ 1, 0 ] += 0.0;
            koC[ 1, 1 ] += ( 2 * koP2.X * koP2.X ) + ( 2 * koP2.Y * koP2.Y );
            koC[ 1, 2 ] +=  2 * koP2.Y;
            koC[ 1, 3 ] += -2 * koP2.X;

            koC[ 2, 0 ] += 2 * koP2.X;
            koC[ 2, 1 ] += 2 * koP2.Y;
            koC[ 2, 2 ] += 2;
            koC[ 2, 3 ] += 0;

            koC[ 3, 0 ] += 2 * koP2.Y;
            koC[ 3, 1 ] += -2 * koP2.X;
            koC[ 3, 2 ] += 0;
            koC[ 3, 3 ] += 2;

            koR[ 0, 0 ] += ( 2 * koP1.X * koP2.X ) + ( 2 * koP1.Y * koP2.Y );
            koR[ 1, 0 ] += ( 2 * koP1.X * koP2.Y ) - ( 2 * koP2.X * koP1.Y );
            koR[ 2, 0 ] += 2 * koP1.X;
            koR[ 3, 0 ] += 2 * koP1.Y;
         }

         // Apply Transformation
         koCi = ( Matrix )koC.Inverse;
         koM = ( Matrix )koCi.Multiply( ( IMatrix )koR );

         koT.VdA  = koM[ 0, 0 ];
         koT.VdB  = koM[ 1, 0 ];
         koT.VdT1 = koM[ 2, 0 ];
         koT.VdT2 = koM[ 3, 0 ];

         koS3 = this.mApplyTransformation( koT, this.voShape2 );

         this.mDisplayShape( this.voShape1, koPen1, koG );
         this.mDisplayShape( koS3,          koPen2, koG );
      }

      private List< Point > mApplyTransformation( Transformation aoT, List< Point > aoShape )
      {
         List< Point > koShape = new List< Point >( aoShape.Count );
         double        kdX;
         double        kdY;

         foreach( Point koPt in aoShape )
         {
            kdX = ( ( double )koPt.X * aoT.VdA ) + ( ( double )koPt.Y * aoT.VdB ) + aoT.VdT1;
            kdY = ( ( double )koPt.X * ( -aoT.VdB ) ) + ( ( double )koPt.Y * aoT.VdA ) + aoT.VdT2;
            koShape.Add( new Point( ( int )kdX, ( int )kdY ) );
         }

         return( koShape );
      }

      private void mDisplayShape( List< Point > aoShape, Pen aoPen, Graphics aoGraphics )
      {
         Point? koPrev = null; // nullable

         foreach( Point koPt in aoShape )
         {
            aoGraphics.DrawEllipse( aoPen, new Rectangle( koPt.X - 2, koPt.Y - 2, 4, 4 ) );
            if( koPrev != null )
            {
               aoGraphics.DrawLine( aoPen, ( Point )koPrev, koPt );
            }
            koPrev = koPt;
         }
         aoGraphics.DrawLine( aoPen, aoShape[ 0 ], aoShape[ aoShape.Count -1 ] );
      }
   }
}
