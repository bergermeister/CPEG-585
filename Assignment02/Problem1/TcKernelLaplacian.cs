namespace Problem1
{
   public class TcKernelLaplacian : TcKernel
   {
      public TcKernelLaplacian( int aiSize ) : base( aiSize )
      {
         int kiRow, kiCol;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;

         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = 1;
            }
         }

         this.vdpM[ kiCenter ][ kiCenter ] = -( this.vdpM.Length * this.vdpM.Length ) + 1;

         mNormalize( ref this.vdpM );
      }
   }
}
