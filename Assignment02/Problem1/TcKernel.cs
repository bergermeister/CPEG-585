using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem1
{
   public class TcKernel
   {
      protected const int    xiMinSize = 3; /**< Minimum Kernel Size */
      protected double[ ][ ] vdpM;          /**< Kernel Matrix */

      public TcKernel( int aiSize )
      {
         int kiSize = aiSize;
         int kiRow;

         if( aiSize < xiMinSize )
         {
            kiSize = aiSize;
         }

         this.vdpM = new double[ kiSize ][ ];
         for( kiRow = 0; kiRow < kiSize; kiRow++ )
         {
            this.vdpM[ kiRow ] = new double[ kiSize ];
         }
      }

      public TcKernel( double[ ][ ] adpKernel )
      {
         int kiRow, kiCol;

         this.vdpM = new double[ adpKernel.Length ][ ];
         for( kiRow = 0; kiRow < adpKernel.Length; kiRow++ )
         {
            this.vdpM[ kiRow ] = new double[ adpKernel.Length ];
            for( kiCol = 0; kiCol < adpKernel.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = adpKernel[ kiRow ][ kiCol ];
            }
         }

         mNormalize( ref this.vdpM );
      }

      ~TcKernel( )
      {
         // TODO: Does vdpM need to be cleaned up?
      }

      /**
       * @note Assumes kernel is symmetric or already rotated by 180 degrees
       * @note The return format is BGR, not RGB
       */ 
      public virtual Bitmap MConvolve( Bitmap aoBmp )
      {
         Bitmap     koBmp = ( Bitmap )aoBmp.Clone( );
         BitmapData koDst = koBmp.LockBits( new Rectangle( 0, 0, koBmp.Width, koBmp.Height ),
                                            ImageLockMode.ReadWrite,
                                            PixelFormat.Format24bppRgb );
         BitmapData koSrc = aoBmp.LockBits( new Rectangle( 0, 0, aoBmp.Width, aoBmp.Height ),
                                            ImageLockMode.ReadWrite,
                                            PixelFormat.Format24bppRgb );

         int    kiStride   = koDst.Stride;
         int    kiCenter = ( this.vdpM.Length - 1 ) / 2;
         IntPtr kipScanDst = koDst.Scan0;
         IntPtr kipScanSrc = koSrc.Scan0;

         unsafe
         {
            byte* kcpDst = ( byte* )( void* )kipScanDst;
            byte* kcpSrc = ( byte* )( void* )kipScanSrc;
            int   kiOffset = kiStride - koBmp.Width * 3;
            int   kiIndex;
            double[ ][ ] kdpMb = new double[ this.vdpM.Length ][ ];
            double[ ][ ] kdpMg = new double[ this.vdpM.Length ][ ];
            double[ ][ ] kdpMr = new double[ this.vdpM.Length ][ ];
            double kdCb, kdCg, kdCr;

            for( kiIndex = 0; kiIndex < this.vdpM.Length; kiIndex++ )
            {
               kdpMb[ kiIndex ] = new double[ this.vdpM.Length ];
               kdpMg[ kiIndex ] = new double[ this.vdpM.Length ];
               kdpMr[ kiIndex ] = new double[ this.vdpM.Length ];
            }

            for( int kiY = 0; kiY < koBmp.Height - ( this.vdpM.Length - 1 ); ++kiY )
            {
               for( int kiX = 0; kiX < koBmp.Width - ( this.vdpM.Length - 1 ); ++kiX )
               {

                  kdCb = 0.0;
                  kdCg = 0.0;
                  kdCr = 0.0;
                  for( int kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
                  {
                     for( int kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
                     {
                        kdpMb[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 0 ];
                        kdpMg[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 1 ];
                        kdpMr[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 2 ];

                        kdCb += kdpMb[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];
                        kdCg += kdpMg[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];
                        kdCr += kdpMr[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];
                     }
                  }
                       
                  if( kdCb <   0 ) kdCb =   0;
                  if( kdCb > 255 ) kdCb = 255;
                  if( kdCg <   0 ) kdCg =   0;
                  if( kdCg > 255 ) kdCg = 255;
                  if( kdCr <   0 ) kdCr =   0;
                  if( kdCr > 255 ) kdCr = 255;

                  kcpDst[ ( kiCenter * 3 ) + ( kiCenter * kiStride ) + 0 ] = ( byte )kdCb;
                  kcpDst[ ( kiCenter * 3 ) + ( kiCenter * kiStride ) + 1 ] = ( byte )kdCg;
                  kcpDst[ ( kiCenter * 3 ) + ( kiCenter * kiStride ) + 2 ] = ( byte )kdCr;

                  kcpDst += 3;
                  kcpSrc += 3;
               }
               kcpDst += kiOffset;
               kcpSrc += kiOffset;
            }
         }

         koBmp.UnlockBits( koDst );
         aoBmp.UnlockBits( koSrc );

         return( koBmp );
      }

      protected static void mNormalize( ref double[ ][ ] adM )
      {
         int    kiRow, kiCol;
         double kdSum = 0.0;

         for( kiRow = 0; kiRow < adM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < adM.Length; kiCol++ )
            {
               kdSum += adM[ kiRow ][ kiCol ];
            }
         }

         if( kdSum != 0.0 )
         { 
            for( kiRow = 0; kiRow < adM.Length; kiRow++ )
            {
               for( kiCol = 0; kiCol < adM.Length; kiCol++ )
               {
                  adM[ kiRow ][ kiCol ] /= kdSum;
               }
            }
         }
      }
   }
}
