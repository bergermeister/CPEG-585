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
      
      public static Bitmap MContrast( Bitmap aoBmp, sbyte acContrast )
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
      
      public static Bitmap MRecoverBitmapFromArray( double[ ] adpBmp, int aiWidth, int aiHeight )
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
 
      public static void MRotateClear( Image aoImg, ref Bitmap aoBmp, double adRot )
      {
         Graphics koDC;

         while( adRot >  360.0 ){ adRot -= 360.0; }
         while( adRot < -360.0 ){ adRot += 360.0; }

         aoBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC = Graphics.FromImage( aoBmp );
         koDC.Clear( Color.White );
         koDC.RotateTransform( ( float )adRot);
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
      }

      public static void MRotateByPoints( Image aoImg, ref Bitmap aorBmp, Point aoP1, Point aoP2 )
      {
         Point  koMid;  // Mid point between P1 and P2
         Point  koNew;  // New mid point
         double kdRad;
         double kdRot;

         if( aoP1.X != aoP2.X )
         {
            koMid = new Point();
            koMid.X = ( int )( ( aoP1.X + aoP2.X ) / 2.0 );
            koMid.Y = ( int )( ( aoP1.Y + aoP2.Y ) / 2.0 );
            kdRad = Math.Atan2( -( aoP2.Y - aoP1.Y ), ( aoP2.X - aoP1.X ) );
            koNew = new Point();
            koNew.X = ( int )( ( koMid.X * Math.Cos( kdRad ) ) - ( koMid.Y * Math.Sin( kdRad ) ) );
            koNew.Y = ( int )( ( koMid.Y * Math.Cos( kdRad ) ) + ( koMid.X * Math.Sin( kdRad ) ) );
            kdRot = kdRad * 180 / 3.141516;
            ImageProcessing.MRotateFill2( aoImg, ref aorBmp, kdRot, koMid, koNew );
         }
      }

      public static void MRotateFill( Image aoImg, ref Bitmap aorBmp, double adRot )
      {
         Graphics koDC;
         double   kdDegrees = adRot * 3.141516 / 180;
         int      kiShift   = ( int )( ( aorBmp.Height / 2 ) * Math.Tan( kdDegrees ) );

         while( adRot >  360.0 ){ adRot -= 360.0; }
         while( adRot < -360.0 ){ adRot += 360.0; }

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koDC.RotateTransform( ( float) adRot );
         if( kdDegrees > 0 )
         {
            koDC.DrawImage( aoImg, new Rectangle( kiShift, -kiShift, aoImg.Width, aoImg.Height ) );
         }
			else
         { 
			   koDC.DrawImage( aoImg, new Rectangle( kiShift, kiShift / 2, aoImg.Width, aoImg.Height ) );
         }
      }

      public static void MRotateFill2( Image aoImg, ref Bitmap aorBmp, double adRot, Point aoOld, Point aoNew )
      {
         Graphics koDC;
         double   kdDeg;
         int      kiShift;

         while (adRot > 360.0) { adRot -= 360.0; }
         while (adRot < -360.0) { adRot += 360.0; }

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koDC.RotateTransform( ( float )adRot );
         kdDeg = adRot * 3.141516 / 180;
         kiShift = ( int )( aorBmp.Height / 2 * Math.Tan( kdDeg ) );
         if( kdDeg > 0 )
         { 
            koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         }
         else
         { 
            koDC.DrawImage( aoImg, new Rectangle( new Point( 0, 0 ), new Size( aoImg.Width, aoImg.Height ) ) );
         }
      }

      public static void MDrawRectangle( Image aoImg, ref Bitmap aorBmp, Rectangle aoRec )
      {
         Graphics koDC;

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koDC.DrawRectangle( new Pen( Color.Red ), aoRec );
      }

      public static void MDrawX( Image aoImg, ref Bitmap aorBmp, Point aoPt )
      {
         Graphics koDC;
         Brush    koBr;
         Point    koP1, koP2, koP3, koP4;

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koBr = new SolidBrush( Color.Red );
         koP1 = new Point( aoPt.X - 3, aoPt.Y - 3 );
         koP2 = new Point( aoPt.X + 3, aoPt.Y + 3 );
         koDC.DrawLine( new Pen( Color.Blue ), koP1, koP2 );
         koP3 = new Point( aoPt.X + 3, aoPt.Y - 3 );
         koP4 = new Point( aoPt.X - 3, aoPt.Y + 3 );
         koDC.DrawLine( new Pen( Color.Blue ), koP3, koP4 );
      }

      public static void Draw2X(Image aoImg, ref Bitmap aorBmp, Point aoPt1, Point aoPt2)
      {
         Graphics koDC;
         Brush    koBr;
         Point    koP1a, koP1b, koP2a, koP2b, koP3a, koP3b, koP4a, koP4b;

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koBr = new SolidBrush( Color.Red );
         koP1a = new Point( aoPt1.X - 3, aoPt1.Y - 3 );
         koP2a = new Point( aoPt1.X + 3, aoPt1.Y + 3 );
         koDC.DrawLine( new Pen( Color.Red ), koP1a, koP2a );
         koP3a = new Point( aoPt1.X + 3, aoPt1.Y - 3 );
         koP4a = new Point( aoPt1.X - 3, aoPt1.Y + 3 );
         koDC.DrawLine(new Pen(Color.Red), koP3a, koP4a);

         koP1b = new Point( aoPt2.X - 3, aoPt2.Y - 3 );
         koP2b = new Point( aoPt2.X + 3, aoPt2.Y + 3 );
         koDC.DrawLine( new Pen( Color.Red ), koP1b, koP2b );
         koP3b = new Point( aoPt2.X + 3, aoPt2.Y - 3 );
         koP4b = new Point( aoPt2.X - 3, aoPt2.Y + 3 );
         koDC.DrawLine( new Pen( Color.Red ), koP3b, koP4b );
      }

      public static void MDraw2Xbold( Image aoImg, ref Bitmap aorBmp, Point aoPt1, Point aoPt2 )
      {
         Graphics koDC;
         Brush    koBr;
         Point    koP1a, koP1b, koP2a, koP2b, koP3a, koP3b, koP4a, koP4b;

         aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
         koBr = new SolidBrush( Color.Red );
         koP1a = new Point( aoPt1.X - 6, aoPt1.Y - 6 );
         koP2a = new Point( aoPt1.X + 6, aoPt1.Y + 6 );
         koDC.DrawLine(new Pen(Color.Red, 2), koP1a, koP2a );
         koP3a = new Point( aoPt1.X + 6, aoPt1.Y - 6 );
         koP4a = new Point( aoPt1.X - 6, aoPt1.Y + 6 );
         koDC.DrawLine( new Pen( Color.Red, 2 ), koP3a, koP4a );

         koP1b = new Point( aoPt2.X - 6, aoPt2.Y - 6 );
         koP2b = new Point( aoPt2.X + 6, aoPt2.Y + 6 );
         koDC.DrawLine( new Pen( Color.Red, 2 ), koP1b, koP2b );
         koP3b = new Point( aoPt2.X + 6, aoPt2.Y - 6 );
         koP4b = new Point( aoPt2.X - 6, aoPt2.Y + 6 );
         koDC.DrawLine( new Pen( Color.Red, 2 ), koP3b, koP4b );
      }

      public static void MResizeImage( Image aoImg, ref Bitmap aorBmp, Rectangle aoRect )
      {
         Graphics koDC;
         aorBmp = new Bitmap( aoRect.Width, aoRect.Height, PixelFormat.Format24bppRgb );
         koDC = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, aoRect);
      }

      public static void MResizeImageProportional( Image aoImg, ref Bitmap aorBmp, Rectangle aoRect )
      {
         Graphics  koDC;
         Rectangle koRect = new Rectangle( aoRect.X, aoRect.Y, aoRect.Width, aoImg.Height * aoRect.Width / aoImg.Width );

         aorBmp = new Bitmap( koRect.Width, koRect.Height, PixelFormat.Format24bppRgb );
         koDC   = Graphics.FromImage( aorBmp );
         koDC.DrawImage( aoImg, koRect );
      }

      public static void MCropImage( Image aoImg, ref Bitmap aorBmp, Rectangle aoRect )
      {
         Bitmap    koCrop = new Bitmap( aoRect.Width, aoRect.Height, aoImg.PixelFormat );
         Graphics  koDC   = Graphics.FromImage( koCrop );
         Rectangle koDest = new Rectangle( 0, 0, aoRect.Width, aoRect.Height );

         koDC.DrawImage( aorBmp, koDest, aoRect.X, aoRect.Y, aoRect.Width, aoRect.Height, GraphicsUnit.Pixel );

         aorBmp = koCrop;
      }

      public static bool MShiftImageHorizontal( Image aoImg, ref Bitmap aorBmp, int aiShiftAmt )
      {
         bool     kbStatus;
         Graphics koDC;

         if( aiShiftAmt > aoImg.Width )
         {
            kbStatus = false;
         }
         else
         { 
            aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
            koDC   = Graphics.FromImage( aorBmp );
            koDC.Clear( Color.Black );
            koDC.TranslateTransform( aiShiftAmt, 0 );
            koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
            kbStatus = true;
         }
         return( kbStatus );
      }

      public static bool MShiftImageVertical( Image aoImg, ref Bitmap aorBmp, int aiShiftAmt )
      {
         bool     kbStatus;
         Graphics koDC;

         if( aiShiftAmt > aoImg.Width )
         {
            kbStatus = false;
         }
         else
         { 
            //aorBmp = new Bitmap( aoImg.Width, aoImg.Height + Math.Abs( aiShiftAmt ), PixelFormat.Format24bppRgb );
            aorBmp = new Bitmap( aoImg.Width, aoImg.Height, PixelFormat.Format24bppRgb );
            koDC   = Graphics.FromImage( aorBmp );
            koDC.Clear( Color.Black );
            koDC.TranslateTransform( 0, aiShiftAmt );
            koDC.DrawImage( aoImg, new Rectangle( 0, 0, aoImg.Width, aoImg.Height ) );
            kbStatus = true;
         }
         
         return( kbStatus );
      }
   }
}
