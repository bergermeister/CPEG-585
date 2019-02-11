namespace Problem1
{
   public class TcKernelHighPass : TcKernel
   {
      public TcKernelHighPass( int aiSize ) : base( aiSize )
      {
         int kiRow, kiCol;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;
         int kiScale  = this.vdpM.Length * this.vdpM.Length;

         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = -1.0;
            }
         }

         this.vdpM[ kiCenter ][ kiCenter ] = kiScale - 1;
      }

   }
}
