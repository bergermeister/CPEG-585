namespace Problem1
{
   public class TcKernelLaplacian : TcKernelGradient
   {
      public TcKernelLaplacian( int aiSize ) : base( aiSize )
      {
         int kiRow, kiCol;
         int kiDx, kiDy;
         int kiCenter = ( this.vdpM.Length - 1 ) / 2;

         kiDy = -kiCenter;
         for( kiRow = 0; kiRow < this.vdpM.Length; kiRow++ )
         {
            kiDx = kiCenter;
            for( kiCol = 0; kiCol < this.vdpM.Length; kiCol++ )
            {
               this.vdpM[ kiRow ][ kiCol ] = ( kiDx * kiDx ) / 4;
               this.vdpY[ kiRow ][ kiCol ] = ( kiDy * kiDy ) / 4;
               kiDx--;
            }
            kiDy++;
         }
      }
   }
}
