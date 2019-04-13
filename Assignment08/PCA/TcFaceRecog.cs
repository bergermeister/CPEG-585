using System;
using System.Collections.Generic;

namespace PCA
{
   public class TcFaceRecog
   {
      private string          voPath;
      private List< TcImage > voImages;
      private TcImage         voImgAvg;

      public TcFaceRecog( string aoPath )
      {
         this.voPath = aoPath;
      }

      public void MComputeEFs( )
      {
         this.voImages = new List< TcImage >( );
      }
   }
}
