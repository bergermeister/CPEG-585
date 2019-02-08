using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem1
{
   public class ImageProcessing
   {
      public ImageProcessing( )
      {
         // TODO: Add constructor logic
      }

      public static Bitmap MConvertToGray( Bitmap aoBmp )
      {        
         int    kiI;
         int    kiJ;
         int    kiGray;
         Color  koColor;
         Bitmap koBmp = new Bitmap( aoBmp.Width, aoBmp.Height );

         for( kiI = 0; kiI < aoBmp.Width; kiI++ )
         {
            for( kiJ = 0; kiJ < aoBmp.Height; kiJ++ )
            {
               // Get the color at the pixel coordinate
               koColor = aoBmp.GetPixel( kiI, kiJ );

               // Convert the RGB color to grayscale
               kiGray = ( byte )( ( 0.299 * koColor.R ) + 
                                  ( 0.587 * koColor.G ) +
                                  ( 0.114 * koColor.B ) );

               // Set the pixel to the grayscale value
               koBmp.SetPixel( kiI, kiJ, Color.FromArgb( kiGray, kiGray, kiGray ) );
            }
         }

         return( koBmp );
      }

      public static Bitmap MBrighten( Bitmap aoUse, int aiBrightness )
      {
         Bitmap koBright;
         Color  koColor;
         int    kiRed;
         int    kiBlue;
         int    kiGreen;
         int    kiRow;
         int    kiCol;

         if( ( aiBrightness < -255 ) || ( aiBrightness > 255 ) )
         {
            koBright = aoUse;
         }
         else
         {
            koBright = new Bitmap( aoUse.Width, aoUse.Height );

            // Iterate through each row in the pixels
            for( kiRow = 0; kiRow < aoUse.Height; kiRow++ )
            {
               // Iterate through each column in the pixels
               for( kiCol = 0; kiCol < aoUse.Width; kiCol++ )
               {
                  // Obtain the pixel at the x/y coordinate
                  koColor = aoUse.GetPixel( kiCol, kiRow );

                  // Adjust the RGB values based on brightness
                  kiRed   = aiBrightness + Convert.ToInt32( koColor.R );
                  kiGreen = aiBrightness + Convert.ToInt32( koColor.G );
                  kiBlue  = aiBrightness + Convert.ToInt32( koColor.B );

                  // Range check RGB values
                  if( kiRed   > 255 ) kiRed   = 255;
                  if( kiRed   <   0 ) kiRed   =   0;
                  if( kiGreen > 255 ) kiGreen = 255;
                  if( kiGreen <   0 ) kiGreen =   0;
                  if( kiBlue  > 255 ) kiBlue  = 255;
                  if( kiBlue <    0 ) kiBlue  =   0;

                  // Set the pixel to the new value
                  koBright.SetPixel( kiCol, kiRow, Color.FromArgb( kiRed, kiGreen, kiBlue ) );
               }
            }
         }

         return( koBright );
      }
      
      public static Bitmap Contrast( Bitmap aoBmp, sbyte acContrast )
      {
         Bitmap koBmp = new Bitmap( aoBmp.Width, aoBmp.Height );
         Color  koColor;
         double kdPixel = 0.0;
         double kdContrast = ( 100.0 + acContrast ) / 100.0;
         int[ ] kipC = new int[ 3 ];
         int    kiI;
         int    kiY;
         int    kiX;

         if( acContrast < -100 ) acContrast = -100;
         if( acContrast >  100 ) acContrast =  100;

         for( kiY = 0; kiY < aoBmp.Width; kiY++ )
         {
            for( kiX = 0; kiX < aoBmp.Width; kiX++ )
            {
               koColor = aoBmp.GetPixel( kiX, kiY );

               kipC[ 0 ] = Convert.ToInt32( koColor.R );
               kipC[ 1 ] = Convert.ToInt32( koColor.G );
               kipC[ 2 ] = Convert.ToInt32( koColor.B );

               for( kiI = 0; kiI < 3; kiI++ )
               {
                  kdPixel = kipC[ kiI ] / 255.0;
                  kdPixel -= 0.5;
                  kdPixel *= kdContrast;
                  if( kdPixel < 0 ) kdPixel = 0;
                  if( kdPixel > 255 ) kdPixel = 255;
                  kipC[ kiI ] = ( int )kdPixel;
               }

               koBmp.SetPixel( kiX, kiY, Color.FromArgb( kipC[ 0 ], kipC[ 1 ], kipC[ 2 ] ) );
            }
         }

         return( koBmp );
      }
      
      public static Bitmap RecoverBitmapFromArray( double[ ] adpBmp, int aiWidth, int aiHeight )
      {
         Bitmap koBmp = new Bitmap( aiWidth, aiHeight );
         int    kiP = 0;
         int    kiQ = 0;
         int    kiI;
         int    kiVal;

         for( kiI = 0; kiI < adpBmp.Length; kiI++ )
         {
            kiVal = ( int )( adpBmp[ kiI ] / ( 1375669f * 900f ) );
            if( kiVal < 0 )
            {
               kiVal = 0;
            }

            koBmp.SetPixel( kiP, kiQ, Color.FromArgb( kiVal, kiVal, kiVal ) );
            kiP++;
            if( kiP == aiWidth )
            {
               kiP = 0;
               kiQ++;
            }
         }

         return( koBmp );
      }
 
      public static void RotateClear( Image aoImg, ref Bitmap aoBmp, double kdRot )
      {
         Graphics koDC;

         while( kdRot >  360.0 ){ kdRot -= 360.0; }
         while( kdRot < -360.0 ){ kdRot += 360.0; }

         aoBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC = Graphics.FromImage( aoBmp );
         koDC.Clear( Color.White );
         koDC.RotateTransform( ( float )kdRot );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
      }
   }
}
