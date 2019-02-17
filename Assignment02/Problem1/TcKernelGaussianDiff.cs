namespace Problem1
{
   public class TcKernelGaussianDiff : TcKernelGaussian
   {
      public TcKernelGaussianDiff( int aiSize, double adSig1, double adSig2 ) : 
         base( aiSize, ( adSig1 < adSig2 ) ? adSig1 : adSig2 )
      {
         double[ ][ ] kdpN;
         int kiRow, kiCol;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;
         int kiOffY, kiOffX;
         int kiScale  = this.vdpM.Length * this.vdpM.Length;

         kdpN = new double[ this.vdpM.Length ][ ];

         kiOffY = -kiCenter;
         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            kdpN[ kiRow ] = new double[ this.vdpM.Length ];
            kiOffX = -kiCenter;
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               kdpN[ kiRow ][ kiCol ]       = this.mGaussian( kiOffX, kiOffY, ( adSig1 > adSig2 ) ? adSig1 : adSig2 );
               this.vdpM[ kiRow ][ kiCol ] -= kdpN[ kiRow ][ kiCol ];
               kiOffX++;               
            }
            kiOffY++;
         }

         mNormalize( ref this.vdpM );
      }
   }
}
