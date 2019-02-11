using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem1
{
   public class TcKernel
   {
      private   const int    xiMinSize = 3; /**< Minimum Kernel Size */
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

      ~TcKernel( )
      {
         // TODO: Does vdpM need to be cleaned up?
      }

      /**
       * @note Assumes kernel is symmetric or already rotated by 180 degrees
       * @note The return format is BGR, not RGB
       */ 
      public Bitmap MConvolve( Bitmap aoBmp )
      {
         Bitmap     koBmp = ( Bitmap )aoBmp.Clone( );
         BitmapData koDst = koBmp.LockBits( new Rectangle( 0, 0, koBmp.Width, koBmp.Height ),
                                            ImageLockMode.ReadWrite,
                                            PixelFormat.Format24bppRgb );
         BitmapData koSrc = aoBmp.LockBits( new Rectangle( 0, 0, aoBmp.Width, aoBmp.Height ),
                                            ImageLockMode.ReadWrite,
                                            PixelFormat.Format24bppRgb );

         int    kiStride   = koDst.Stride;
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

            for( int kiY = 0; kiY < koBmp.Height - 2; ++kiY )
            {
               for( int kiX = 0; kiX < koBmp.Width - 2; ++kiX )
               {
                  kdpMb[ 0 ][ 0 ] = kcpSrc[ 0 ];
                  kdpMg[ 0 ][ 0 ] = kcpSrc[ 1 ];
                  kdpMr[ 0 ][ 0 ] = kcpSrc[ 2 ];

                  kdpMb[ 0 ][ 1 ] = kcpSrc[ 3 ];
                  kdpMg[ 0 ][ 1 ] = kcpSrc[ 4 ];
                  kdpMr[ 0 ][ 1 ] = kcpSrc[ 5 ];

                  kdpMb[ 0 ][ 2 ] = kcpSrc[ 6 ];
                  kdpMg[ 0 ][ 2 ] = kcpSrc[ 7 ];
                  kdpMr[ 0 ][ 2 ] = kcpSrc[ 8 ];

                  kdpMb[ 1 ][ 0 ] = kcpSrc[ 0 + kiStride ];
                  kdpMg[ 1 ][ 0 ] = kcpSrc[ 1 + kiStride ];
                  kdpMr[ 1 ][ 0 ] = kcpSrc[ 2 + kiStride ];

                  kdpMb[ 1 ][ 1 ] = kcpSrc[ 3 + kiStride ];
                  kdpMg[ 1 ][ 1 ] = kcpSrc[ 4 + kiStride ];
                  kdpMr[ 1 ][ 1 ] = kcpSrc[ 5 + kiStride ];

                  kdpMb[ 1 ][ 2 ] = kcpSrc[ 6 + kiStride ];
                  kdpMg[ 1 ][ 2 ] = kcpSrc[ 7 + kiStride ];
                  kdpMr[ 1 ][ 2 ] = kcpSrc[ 8 + kiStride ];

                  kdpMb[ 2 ][ 0 ] = kcpSrc[ 0 + kiStride * 2 ];
                  kdpMg[ 2 ][ 0 ] = kcpSrc[ 1 + kiStride * 2 ];
                  kdpMr[ 2 ][ 0 ] = kcpSrc[ 2 + kiStride * 2 ];

                  kdpMb[ 2 ][ 1 ] = kcpSrc[ 3 + kiStride * 2 ];
                  kdpMg[ 2 ][ 1 ] = kcpSrc[ 4 + kiStride * 2 ];
                  kdpMr[ 2 ][ 1 ] = kcpSrc[ 5 + kiStride * 2 ];

                  kdpMb[ 2 ][ 2 ] = kcpSrc[ 6 + kiStride * 2 ];
                  kdpMg[ 2 ][ 2 ] = kcpSrc[ 7 + kiStride * 2 ];
                  kdpMr[ 2 ][ 2 ] = kcpSrc[ 8 + kiStride * 2 ];

                       
                  kdCb = kdpMb[ 0 ][ 0 ] * this.vdpM[ 0 ][ 0 ] +
                         kdpMb[ 0 ][ 1 ] * this.vdpM[ 0 ][ 1 ] +
                         kdpMb[ 0 ][ 2 ] * this.vdpM[ 0 ][ 2 ] +
                         kdpMb[ 1 ][ 0 ] * this.vdpM[ 1 ][ 0 ] +
                         kdpMb[ 1 ][ 1 ] * this.vdpM[ 1 ][ 1 ] +
                         kdpMb[ 1 ][ 2 ] * this.vdpM[ 1 ][ 2 ] +
                         kdpMb[ 2 ][ 0 ] * this.vdpM[ 2 ][ 0 ] +
                         kdpMb[ 2 ][ 1 ] * this.vdpM[ 2 ][ 1 ] +
                         kdpMb[ 2 ][ 2 ] * this.vdpM[ 2 ][ 2 ];

                  kdCg = kdpMg[ 0 ][ 0 ] * this.vdpM[ 0 ][ 0 ] +
                         kdpMg[ 0 ][ 1 ] * this.vdpM[ 0 ][ 1 ] +
                         kdpMg[ 0 ][ 2 ] * this.vdpM[ 0 ][ 2 ] +
                         kdpMg[ 1 ][ 0 ] * this.vdpM[ 1 ][ 0 ] +
                         kdpMg[ 1 ][ 1 ] * this.vdpM[ 1 ][ 1 ] +
                         kdpMg[ 1 ][ 2 ] * this.vdpM[ 1 ][ 2 ] +
                         kdpMg[ 2 ][ 0 ] * this.vdpM[ 2 ][ 0 ] +
                         kdpMg[ 2 ][ 1 ] * this.vdpM[ 2 ][ 1 ] +
                         kdpMg[ 2 ][ 2 ] * this.vdpM[ 2 ][ 2 ];

                  kdCr = kdpMr[ 0 ][ 0 ] * this.vdpM[ 0 ][ 0 ] +
                         kdpMr[ 0 ][ 1 ] * this.vdpM[ 0 ][ 1 ] +
                         kdpMr[ 0 ][ 2 ] * this.vdpM[ 0 ][ 2 ] +
                         kdpMr[ 1 ][ 0 ] * this.vdpM[ 1 ][ 0 ] +
                         kdpMr[ 1 ][ 1 ] * this.vdpM[ 1 ][ 1 ] +
                         kdpMr[ 1 ][ 2 ] * this.vdpM[ 1 ][ 2 ] +
                         kdpMr[ 2 ][ 0 ] * this.vdpM[ 2 ][ 0 ] +
                         kdpMr[ 2 ][ 1 ] * this.vdpM[ 2 ][ 1 ] +
                         kdpMr[ 2 ][ 2 ] * this.vdpM[ 2 ][ 2 ];

                  if( kdCb <   0 ) kdCb =   0;
                  if( kdCb > 255 ) kdCb = 255;
                  if( kdCg <   0 ) kdCg =   0;
                  if( kdCg > 255 ) kdCg = 255;
                  if( kdCr <   0 ) kdCr =   0;
                  if( kdCr > 255 ) kdCr = 255;

                  kcpDst[ 3 + kiStride ] = ( byte )kdCb;
                  kcpDst[ 4 + kiStride ] = ( byte )kdCg;
                  kcpDst[ 5 + kiStride ] = ( byte )kdCr;

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
   }
}
