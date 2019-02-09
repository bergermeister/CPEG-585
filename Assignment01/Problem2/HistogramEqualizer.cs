using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem2
{
   public class HistogramEqualizer
   {
      public HistogramEqualizer( )
      {
         // TODO: Implement Constructor
      }

      public Bitmap MProcess( Bitmap aoBmp )
      {
         Bitmap    koBmp  = new Bitmap( aoBmp );
         Histogram koHist = new Histogram( aoBmp );  
         Color     koPixel;

         int kiX;
         int kiY;
         int kiR;
         int kiG;
         int kiB;

         long kiSize = koBmp.Width * koBmp.Height;
         long kiCdfR = koHist.VipR[ 0 ];
         long kiCdfG = koHist.VipG[ 0 ];
         long kiCdfB = koHist.VipB[ 0 ];

         double[ ] kdHistR = new double[ Histogram.XiCount ];
         double[ ] kdHistG = new double[ Histogram.XiCount ];
         double[ ] kdHistB = new double[ Histogram.XiCount ];
         
         kdHistR[ 0 ] = ( koHist.VipR[ 0 ] * Histogram.XiCount ) / kiSize;
         kdHistG[ 0 ] = ( koHist.VipG[ 0 ] * Histogram.XiCount ) / kiSize; 
         kdHistB[ 0 ] = ( koHist.VipB[ 0 ] * Histogram.XiCount ) / kiSize;

         for( kiX = 1; kiX < Histogram.XiCount; kiX++ )
         {
            kiCdfR += koHist.VipR[ kiX ];
            kdHistR[ kiX ] = ( kiCdfR * Histogram.XiCount ) / kiSize;

            kiCdfG += koHist.VipG[ kiX ];
            kdHistG[ kiX ] = ( kiCdfG * Histogram.XiCount ) / kiSize;

            kiCdfB += koHist.VipB[ kiX ];
            kdHistB[ kiX ] = ( kiCdfB * Histogram.XiCount ) / kiSize;
         }
         
         for( kiY = 0; kiY < koBmp.Height; kiY++ )
         {
            for( kiX = 0; kiX < koBmp.Width; kiX++ )
            {
               koPixel = aoBmp.GetPixel( kiX, kiY );

               // Get the RGB values
               kiR = koPixel.R;
               kiG = koPixel.G;
               kiB = koPixel.B;

               kiR = ( int )kdHistR[ kiR ];
               kiG = ( int )kdHistG[ kiG ];
               kiB = ( int )kdHistB[ kiB ];

               // Range check RGB values
               if( kiR > 255 ) kiR = 255;
               if( kiR <   0 ) kiR =   0;
               if( kiG > 255 ) kiG = 255;
               if( kiG <   0 ) kiG =   0;
               if( kiB > 255 ) kiB = 255;
               if( kiB <   0 ) kiB =   0;

               koBmp.SetPixel( kiX, kiY, Color.FromArgb( kiR, kiG, kiB ) );
            }
         }

         return( koBmp );
      }
   }
}
