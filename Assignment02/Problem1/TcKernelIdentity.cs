namespace Problem1
{
   public class TcKernelIdentity : TcKernel
   {
      public TcKernelIdentity( int aiSize ) : base( aiSize )
      {
         int kiRow, kiCol;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;

         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < this.vdpM[ kiRow ].Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = 0;
            }
         }

         this.vdpM[ kiCenter ][ kiCenter ] = 1;

         mNormalize( ref this.vdpM );
      }
   }
}
