namespace Problem1
{
   public class TcKernelSharpen : TcKernel
   {
      public TcKernelSharpen( int aiSize, double adP ) : base( aiSize )
      {
         int    kiRow, kiCol;
         double kdP   = adP;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;
         int kiScale  = this.vdpM.Length * this.vdpM.Length;

         if( adP > 1.0 ) kdP = 1.0;
         if( adP < 0.0 ) kdP = 0.0;

         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = -kdP;
            }
         }

         this.vdpM[ kiCenter ][ kiCenter ] = ( double )kiScale - kdP;

         mNormalize( ref this.vdpM );
      }
   }
}
