namespace LDA
{
   using System.Collections.Generic;
   using System.IO;

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

         /// -# Execute LDA on the class set
         this.mExecLDA( );
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
         this.voSampleSize = this.voImages.Count - this.voClasses.Count;

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

      public TcImage MReconstruct( TcImage aoImg, ref TcMatch[ ] aoMatchesPCA, ref TcMatch[ ] aoMatchesLDA )
      {
         /// Create empty reconstructed Image
         TcImage    koRec = new TcImage( aoImg.ViWidth, aoImg.ViHeight );  

         /// -# Reconstruct the image and determine matches using PCA
         koRec = this.voPCA.MReconstruct( aoImg, ref aoMatchesPCA ); 

         /// -# Determine matches using LDA
         this.voLDA.MMatches( aoImg, ref aoMatchesLDA );

         return( koRec );
      }
   }
}
      