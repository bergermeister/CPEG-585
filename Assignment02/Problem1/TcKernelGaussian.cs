using System;

namespace Problem1
{
   public class TcKernelGaussian : TcKernel
   {
      public TcKernelGaussian( int aiSize, double adSigma ) : base( aiSize )
      {
         int kiRow, kiCol;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;
         int kiOffY, kiOffX;
         int kiScale  = this.vdpM.Length * this.vdpM.Length;

         kiOffY = -kiCenter;
         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            kiOffX = -kiCenter;
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = this.mGaussian( kiOffX, kiOffY, adSigma );
               kiOffX++;               
            }
            kiOffY++;
         }
      }

      protected double mGaussian( double adX, double adY, double adSigma )
      {
         double kdExp = -( ( adX * adX ) + ( adY * adY ) ) / ( 2 * adSigma * adSigma );
         double kdA   =  1 / ( 2 * Math.PI * adSigma * adSigma );
         return( kdA * Math.Pow( Math.E, kdExp ) );
      }
   }
}
