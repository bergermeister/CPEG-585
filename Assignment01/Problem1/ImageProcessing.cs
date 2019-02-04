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

      public static bool MConvertToGray( Bitmap aoBmp )
      {        
         int   kiI;
         int   kiJ;
         int   kiGray;
         Color koColor;

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
               aoBmp.SetPixel( kiI, kiJ, Color.FromArgb( kiGray, kiGray, kiGray ) );
            }
         }

         return( true );
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
      /*
      public static Bitmap Contrast( Bitmap aoBmp, sbyte acContrast )
      {

      }
      */
   }
}
