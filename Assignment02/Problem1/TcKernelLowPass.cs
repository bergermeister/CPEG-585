namespace Problem1
{
   public class TcKernelLowPass : TcKernel
   {
      public TcKernelLowPass( int aiSize ) : base( aiSize )
      {
         int kiRow, kiCol;

         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < this.vdpM[ kiRow ].Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = 1.0;
            }
         }

         mNormalize( ref this.vdpM );
      }
   }
}
