using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Problem1
{
   public class TcKernelGradient : TcKernel
   {
      protected double[ ][ ] vdpY;          /**< Kernel Matrix dP/dy */

      public TcKernelGradient( int aiSize ) : base( aiSize )
      {
         int kiSize = aiSize;
         int kiRow, kiCol;
         int kiDx, kiDy;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;

         if( aiSize < xiMinSize )
         {
            kiSize = aiSize;
         }

         this.vdpY = new double[ kiSize ][ ];
         for( kiRow = 0; kiRow < kiSize; kiRow++ )
         {
            this.vdpY[ kiRow ] = new double[ kiSize ];
         }

         kiDy = -kiCenter;
         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            kiDx = kiCenter;
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = kiDx;
               this.vdpY[ kiRow ][ kiCol ] = kiDy;
               kiDx--;
            }
            kiDy++;
         }

         mNormalize( ref this.vdpM );
         mNormalize( ref this.vdpY );
      }

      public override Bitmap MConvolve( Bitmap aoBmp )
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
            double kdCbX, kdCgX, kdCrX;
            double kdCbY, kdCgY, kdCrY;
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

                  kdCbX = 0.0;
                  kdCgX = 0.0;
                  kdCrX = 0.0;
                  kdCbY = 0.0;
                  kdCgY = 0.0;
                  kdCrY = 0.0;
                  for( int kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
                  {
                     for( int kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
                     {
                        kdpMb[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 0 ];
                        kdpMg[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 1 ];
                        kdpMr[ kiRow ][ kiCol ] = kcpSrc[ ( 3 * kiCol ) + ( kiStride * kiRow ) + 2 ];

                        // Apply dP/dx
                        kdCbX += kdpMb[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];
                        kdCgX += kdpMg[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];
                        kdCrX += kdpMr[ kiRow ][ kiCol ] * this.vdpM[ kiRow ][ kiCol ];

                        // Apply dP/dy
                        kdCbY += kdpMb[ kiRow ][ kiCol ] * this.vdpY[ kiRow ][ kiCol ];
                        kdCgY += kdpMg[ kiRow ][ kiCol ] * this.vdpY[ kiRow ][ kiCol ];
                        kdCrY += kdpMr[ kiRow ][ kiCol ] * this.vdpY[ kiRow ][ kiCol ];
                     }
                  }

                  // Calculate Gradient
                  kdCb = Math.Sqrt( ( kdCbX * kdCbX ) + ( kdCbY * kdCbY ) );
                  kdCg = Math.Sqrt( ( kdCgX * kdCgX ) + ( kdCgY * kdCgY ) );
                  kdCr = Math.Sqrt( ( kdCrX * kdCrX ) + ( kdCrY * kdCrY ) );
                       
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
   }
}
