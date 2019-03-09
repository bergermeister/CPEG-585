using System.Collections.Generic;
using System.Drawing;

namespace OutlierRemoval
{
   public class Transformation
   {
      public double VdA { get; set; }
      public double VdB { get; set; }
      public double VdT1{ get; set; }
      public double VdT2{ get; set; }

      public Transformation( )
      {

      }

      public List< Point > MApply( List< Point > aoShape )
      {
         List< Point > koShape = new List< Point >( aoShape.Count );
         double        kdX;
         double        kdY;

         foreach( Point koPt in aoShape )
         {
            kdX = ( ( double )koPt.X * this.VdA ) + ( ( double )koPt.Y * this.VdB ) + this.VdT1;
            kdY = ( ( double )koPt.X * ( -this.VdB ) ) + ( ( double )koPt.Y * this.VdA ) + this.VdT2;
            koShape.Add( new Point( ( int )kdX, ( int )kdY ) );
         }

         return( koShape );
      }
   }
}
