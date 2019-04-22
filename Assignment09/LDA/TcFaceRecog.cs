namespace LDA
{
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;

   public class TcFaceRecog
   {
      private string                            voPath;
      private int                               voSampleSize;
      private PCA.TcPCA                         voPCA;
      private LDA.TcLDA                         voLDA;
      private List< TcImage >                   voImages;
      private Dictionary< string, LDA.TcClass > voClasses;

      public TcFaceRecog( string aoPath )
      {
         /// -# Record the path
         this.voPath = aoPath;
      }

      public PCA.TcPCA VoPCA
      {
         get{ return( this.voPCA ); }
      }

      public LDA.TcLDA VoLDA
      {
         get{ return( this.voLDA ); }
      }

      public List< TcImage > VoImages
      {
         get{ return( this.voImages ); }
      }

      public Dictionary< string, LDA.TcClass > VoClasses
      {
         get{ return( this.voClasses ); }
      }

      public void MTrain( )
      {
         /// -# Read in the images and initialize class dictionary
         this.mReadImages( );

         /// -# Execute PCA on the image set
         this.mExecPCA( );

         foreach( TcImage koImg in this.voImages )
         {
            koImg.VdVecRdc = koImg.VdVecFSV;
            //koImg.VdVecRdc = this.mNormalize( koImg.VdVecFSV );
         }

         /// -# Execute LDA on the class set
         this.mExecLDA( );
      }

      public TcImage MReconstruct( TcImage aoImg, ref TcMatch[ ] aoMatchesPCA, ref TcMatch[ ] aoMatchesLDA )
      {
         /// Create empty reconstructed Image
         TcImage koRec;

         /// -# Reconstruct the image and determine matches using PCA
         koRec = this.voPCA.MReconstruct( aoImg, ref aoMatchesPCA ); 

         aoImg.VdVecRdc = aoImg.VdVecFSV;
         //aoImg.VdVecRdc = this.mNormalize( aoImg.VdVecFSV );

         /// -# Determine matches using LDA
         this.voLDA.MMatches( aoImg, ref aoMatchesLDA );

         return( koRec );
      }

      private void mReadImages( )
      {
         TcImage koImg;

         /// -# Create a new list of images and dictionary of classes
         this.voImages  = new List< TcImage >( );
         this.voClasses = new Dictionary< string, LDA.TcClass >( );

         /// -# Iterate through the files in the directory 
         foreach( string koFilename in Directory.EnumerateFiles( this.voPath ) )
         {
            /// -# Create a new Image object from the file
            koImg = new TcImage( koFilename );

            /// -# Add the image to the list of images
            this.voImages.Add( koImg );

            /// -# If the image belongs to a new class, create a new class in the dictionary
            if( !this.voClasses.ContainsKey( koImg.VoId ) )
            {
               this.voClasses.Add( koImg.VoId, new LDA.TcClass( koImg.VoId ) );
            }

            /// -# Add the image to the class sample list
            this.voClasses[ koImg.VoId ].Add( koImg );
         }
      }
   
      private void mExecPCA( )
      {         
         /// -# Calculate the Sample Size: Eigen Faces = Image Count - Class Count
         //this.voSampleSize = this.VoImages.Count; 
         //this.voSampleSize = ( this.voImages.Count - this.voClasses.Count );
         this.voSampleSize = this.voClasses.Count + 39;

         /// -# Initialize PCA with the image data and Eigen Face count
         this.voPCA = new PCA.TcPCA( voImages, this.voSampleSize );

         /// -# Execute PCA
         this.voPCA.MTrain( );
      }

      private void mExecLDA( )
      {
         /// -# Create a new LDA object
         this.voLDA = new LDA.TcLDA( this.voImages, this.voClasses, this.voSampleSize );

         /// -# Execute the LDA
         this.voLDA.MTrain( );
      }

      private double[ ] mNormalize( double[ ] adData )
      {
         double[ ] kdData = new double[ adData.Length ];
         double    kdMax  = ( from kdN in adData select kdN ).Max( );
         double    kdMin  = ( from kdN in adData select kdN ).Min( );
         double    kdDlt  = kdMax - kdMin;
         int       kiI;
         

         for( kiI = 0; kiI < kdData.Length; kiI++ )
         {
            kdData[ kiI ] = kdData[ kiI ] - kdMin;
            kdData[ kiI ] = ( kdData[ kiI ] / kdDlt ) * 255.0;
            if( kdData[ kiI ] < 0 )
            {
               kdData[ kiI ] = 0;
            }
            if( kdData[ kiI ] > 255.0 )
            {
               kdData[ kiI ] = 255.0;
            }
         }

         return( kdData );
      }
   }
}
      