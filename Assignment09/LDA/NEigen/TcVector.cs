using System;

namespace LDA.NEigen
{
   public class TcVector : IComparable, ICloneable
   {
      private double    vdValue;    /**< Eigen Value */
      private double[ ] vdData;     /**< Eigen Vector */
      private int       viLength;   /**< Length of Eigen Vector */

      public double[ ] VdData{ get{ return( this.vdData ); } }

      public TcVector( )
      {

      }

      public TcVector( double adValue, double[ ] adData, int aiLength )
      {
         int kiI;

         /// -# Initialize the Eigen Value
         this.vdValue = adValue;

         /// -# Initialize the Eigen Vector Length
         this.viLength = aiLength;

         /// -# Copy the Eigen Vector data
         this.vdData = new double[ this.viLength ];
         for( kiI = 0; kiI < this.viLength; kiI++ )
         {
            this.vdData[ kiI ] = adData[ kiI ];
         }         
      }

      public int CompareTo( Object aoRHS )
      {
         TcVector koEV = aoRHS as TcVector;
         return( ( koEV.vdValue * koEV.vdValue ).CompareTo( this.vdValue * this.vdValue ) );
      }

      public object Clone( )
      {
         TcVector koClone = new TcVector( );

         /// -# Copy the Eigen Value
         koClone.vdValue = this.vdValue;

         /// -# Copy the Eigen Vector
         if( this.vdData != null )
         {
            koClone.vdData = ( double[ ] )this.vdData.Clone( );
         }

         /// -# Copy the length of the Eigen Vector
         koClone.viLength = this.viLength;

         return( koClone );
      }
   }
}
