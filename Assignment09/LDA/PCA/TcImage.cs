using System;
using System.Drawing;
using System.IO;
using FaceRecogPCA;
  
namespace PCA
{
   public class TcImage
   {
      private string    voPath;     /**< Full Path to Image */
      private string    voName;     /**< Image File Name */
      private string    voId;       /**< Image Identifier */
      private Bitmap    voBmp;      /**< Image Bitmap  */
      private double[ ] vdVec;      /**< Linearized Image Vector */
      private double[ ] vdVecAdj;   /**< Mean Adjusted Vector */
      private double[ ] vdVecFSV;   /**< Face Space Vector (Projection onto reduced Dimension) */

      public string     VoId    { get{ return( this.voId ); } }
      public int        ViWidth { get{ return( this.voBmp.Width  ); } }
      public int        ViHeight{ get{ return (this.voBmp.Height ); } }
      public double[ ]  VdVec   { get{ return( this.vdVec ); } }
      public double[ ]  VdVecAdj{ get{ return( this.vdVecAdj ); } }
      public double[ ]  VdVecFSV{ get{ return( this.vdVecFSV ); } set{ this.vdVecFSV = value; } }
      public Bitmap     VoBmp   { get{ return( this.voBmp ); } }
      public string     VoName  { get{ return( this.voName ); } }

      public TcImage( )
      {

      }

      public TcImage( int aiWidth, int aiHeight, double[ ] adData = null )
      {
         int kiI;

         /// -# Allocate space for linearized image
         this.vdVec    = new double[ aiHeight * aiWidth ];
         this.vdVecAdj = new double[ aiHeight * aiWidth ];

         /// -# Create Bitmap
         this.voBmp = new Bitmap( aiWidth, aiHeight );

         /// -# If data is not provided, zero out linearized image data
         if( adData == null )
         { 
            for( kiI = 0; kiI < this.vdVec.Length; kiI++ )
            {
               this.vdVec[ kiI ] = 0.0;
            }
         }
         /// -# Else if data is provided, copy the data into the linearized image data and update the bitmap
         else
         {
            for( kiI = 0; kiI < this.vdVec.Length; kiI++ )
            {
               this.vdVec[ kiI ] = adData[ kiI ];
            }
            this.MUpdateBitmap( );
         }
      }

      public TcImage( string aoFile )
      {
         FileInfo koInfo = new FileInfo(aoFile);

         this.voPath = aoFile;
         this.voName = koInfo.Name;
         this.voId   = koInfo.Name.Substring( 0, 3 );
         this.mReadImage( );
      }

      public void MUpdateBitmap( )
      {
         int kiX, kiY, kiI;

         kiI = 0;
         for( kiY = 0; kiY < this.voBmp.Height; kiY++ )
         {
            for( kiX = 0; kiX < this.voBmp.Width; kiX++ )
            {
               this.voBmp.SetPixel( kiX, kiY, Color.FromArgb( ( int )this.vdVec[ kiI ], 
                                                              ( int )this.vdVec[ kiI ], 
                                                              ( int )this.vdVec[ kiI ] ) );
               kiI++;
            }
         }
      }

      private void mReadImage( )
      {
         Color koC;
         int   kiX, kiY, kiI;

         this.voBmp    = new Bitmap( this.voPath );                           /// -# Read the image into a Bitmap
         this.vdVec    = new double[ this.voBmp.Height * this.voBmp.Width ];  /// -# Create a vector for the linearized image
         this.vdVecAdj = new double[ this.voBmp.Height * this.voBmp.Width ];  /// -# Create a vector for the mean adjusted linearized image

         kiI = 0;
         for( kiY = 0; kiY < this.voBmp.Height; kiY++ )
         {
            for( kiX = 0; kiX < this.voBmp.Width; kiX++ )
            {
               // Obtain the pixel color
               koC = this.voBmp.GetPixel(kiX, kiY);

               // Store the pixel color as grayscale
               this.vdVec[ kiI ] = ( ( 0.299 * koC.R ) + ( 0.587 * koC.G ) + ( 0.114 * koC.B ) );

               kiI++;
            }
         }
      }
   }
}
