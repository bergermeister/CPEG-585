using System;
using System.Drawing;
using FaceRecogPCA;
  
namespace PCA
{
   public class TcImage
   {
      private string    voId;       /**< Image Identifier */
      private Bitmap    voBmp;      /**< Image Bitmap  */
      private double[ ] vdVec;      /**< Linearized Image Vector */
      private double[ ] vdVecAdj;   /**< Mean Adjusted Vector */
      private double[ ] vdVecFS;    /**< Face Space Vector */

      public string VoId{ get{ return( this.voId ); } }

      public TcImage( )
      {

      }

      public 
   }
}
