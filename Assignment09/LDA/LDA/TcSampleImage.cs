namespace LDA.LDA
{
   using System.Drawing;

   public class TcSampleImage : TcSample
   {
      private string voPath;
      private Bitmap voBmp;

      public TcSampleImage( string aoPath ) : base( )
      {
         this.voPath = aoPath;
         this.voBmp  = new Bitmap( aoPath );
         this.vdData = new double[ this.voBmp.Width * this.voBmp.Height ];

         this.mLinearize( );
      }

      private void mLinearize( )
      {
         Color koC;
         int   kiX, kiY, kiI;

         kiI = 0;
         for( kiY = 0; kiY < this.voBmp.Height; kiY++ )
         { 
            for( kiX = 0; kiX < this.voBmp.Width; kiX++ )
            {
               /// -# Obtain each pixel
               koC = this.voBmp.GetPixel( kiX, kiY );

               /// -# Convert each pixel to grayscale value
               this[ kiI ] = ( ( 0.299 * koC.R ) + ( 0.587 * koC.G ) + ( 0.114 * koC.B ) );

               kiI++;
            }
         }
      }
   }
}
