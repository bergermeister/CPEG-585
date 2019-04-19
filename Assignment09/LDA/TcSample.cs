using System;

namespace LDA
{
   public class TcSample
   {
      protected double[ ] vdData;

      public TcSample( )
      {
         this.vdData = null;
      }

      public TcSample( int aiLength )
      {
         this.vdData = new double[ aiLength ];
      }

      public TcSample( double[ ] adData )
      {
         this.vdData = adData;
      }

      public double this[int aiIndex]
      {
         get{ return( this.vdData[ aiIndex ] ); }
         set{ this.vdData[aiIndex] = value; }
      }

      public int ViLength
      {
         get{ return( this.vdData.Length ); }
      }
   }
}
