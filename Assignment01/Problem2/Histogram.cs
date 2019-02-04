using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem2
{
   public class Histogram
   {
      public const int XiCount = 256;
      private int[ ] vipR;
      private int[ ] vipG;
      private int[ ] vipB;

      public int[ ] VipR
      {
         get{ return( this.vipR ); }
      }

      public int[ ] VipG
      {
         get{ return( this.vipG ); }
      }

      public int[ ] VipB
      {
         get{ return( this.vipB ); }
      }

      public Histogram( )
      {
         this.vipR = new int[ XiCount ];
         this.vipG = new int[ XiCount ];
         this.vipB = new int[ XiCount ];
      }

      public Histogram( Bitmap aoBmp )
      {
         this.vipR = new int[ XiCount ];
         this.vipG = new int[ XiCount ];
         this.vipB = new int[ XiCount ];
         this.MInitialize( aoBmp );
      }

      public void MInitialize( Bitmap aoBmp )
      {
         int    kiX;
         int    kiY;
         Color  koColor;

         for( kiX = 0; kiX < XiCount; kiX++ )
         {
            this.vipR[ kiX ] = 0;
            this.vipG[ kiX ] = 0;
            this.vipB[ kiX ] = 0;
         }

         for( kiY = 0; kiY < aoBmp.Height; kiY++ )
         {
            for( kiX = 0; kiX < aoBmp.Width; kiX++ )
            {
               koColor = aoBmp.GetPixel( kiX, kiY );
               this.vipR[ koColor.R ]++;
               this.vipG[ koColor.G ]++;
               this.vipB[ koColor.B ]++;
            }
         }
      }
   }
}
