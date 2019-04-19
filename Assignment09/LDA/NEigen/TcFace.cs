using System;

namespace LDA.NEigen
{
   public class TcFace : ICloneable
   {
      private double    vdValue;    /**< Eigen Value */
      private double[ ] vdData;     /**< Eigen Face Data */
      private double[ ] vdVar2;     /**< Variance Squared */
      private int       viLength;   /**< Length of Eigen Face Data */

      public double[ ] VdData{ get{ return( this.vdData ); } }
      public double[ ] VdVar2{ get{ return( this.vdVar2 ); } }

      public TcFace( )
      {

      }

      public TcFace( int aiLength )
      {
         this.viLength = aiLength;
         this.vdData = new double[ this.viLength ];
         this.vdVar2 = new double[ this.viLength ];
      }

      public object Clone( )
      {
         TcFace koCopy = new TcFace( );

         /// -# Copy the Eigen Value
         koCopy.vdValue  = this.vdValue;

         /// -# Copy the length of the Eigen Face Data
         koCopy.viLength = this.viLength;

         /// -# Copy the Eigen Face Data
         if( this.vdData != null )
         {
            koCopy.vdData = ( double[ ] )this.vdData.Clone( );
         }

         /// -# Copy the Variance
         if( this.vdVar2 != null )
         {
            koCopy.vdVar2 = ( double[ ] )this.vdVar2.Clone( );
         }

         return( koCopy );
      }
   }
}
