using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using FaceRecogPCA;
using PCA.NEigen;

namespace PCA
{
   public class TcFaceRecog
   {
      private string           voPath;    /**< Path to training data */
      private int              viCountEF; /**< Number of Eigen Faces */
      private List< TcImage >  voImages;  /**< Collection of Images */
      private TcImage          voImgAvg;  /**< Average Image */
      private Matrix           voMatAdj;  /**< Matrix of Mean Adjusted Image Vectors */
      private Matrix           voMatCov;  /**< Covariance Matrix */
      private Matrix           voMatEF;   /**< Eigen Face Matrix */
      private List< TcVector > voEV;      /**< List of Eigen Vectors */ 
      private List< TcFace   > voEF;      /**< List of Eigen Faces */

      public List< TcImage > VoImages{ get{ return( this.voImages ); } }
      public TcImage         VoImgAvg{ get{ return( this.voImgAvg ); } }
      public List< TcFace >  VoEF    { get{ return( this.voEF ); } }

      public TcFaceRecog( string aoPath, int aiCountEF )
      {
         this.voPath    = aoPath;
         this.viCountEF = aiCountEF;
      }

      public void MAdjustImage( TcImage aoImg )
      {
         int kiI;

         for( kiI = 0; kiI < this.voImgAvg.VdVec.Length; kiI++ )
         {
            aoImg.VdVecAdj[ kiI ] = aoImg.VdVec[ kiI ] - this.voImgAvg.VdVec[ kiI ];
         }  
      }

      public void MTrain( )
      {
         this.voImages = new List< TcImage >( );

         try
         { 
            /// -# Read all images into the list
            foreach( string koFile in Directory.EnumerateFiles( this.voPath ) )
            {
               this.voImages.Add( new TcImage( koFile ) );
            }

            /// -# Compute Average Image
            this.mComputeAvgImg( );

            /// -# Compute Mean Adjusted Images
            this.mAdjustImages( );

            /// -# Copy Mean Adjusted Image Vectors into Mean Adjusted Matrix
            this.mInitMatAdj( );

            /// -# Compute the Covariance Matrix
            this.voMatCov = ( Matrix )( this.voMatAdj.Transpose( ).Multiply( this.voMatAdj ) );

            /// -# Compute the Eigen Values and Eigen Vectors
            this.mComputeEV( );

            /// -# Compute the Eigen Faces
            this.mComputeEF( );

            /// -# Project images onto reduced dimensions
            this.mComputeFS( );
         }
         catch( Exception aoException )
         {
            Console.WriteLine( aoException.Message );
         }
      }

      public TcImage MReconstruct( TcImage aoImg, ref TcMatch[ ] aoMatches )
      {
         /// Create empty reconstructed Image
         TcMatch[ ] koMatches;
         TcImage koRec = new TcImage( aoImg.ViWidth, aoImg.ViHeight );  
         int kiI, kiJ;
         double kdMax;
         double kdMin;
         double kdDlt;
         double kdRecErr;
         double kdDist;

         /// -# Subtact mean image from input image
         this.MAdjustImage( aoImg );

         /// -# Compute Face Space Vector
         this.mComputeFS( aoImg );

         /// -# Reconstruct the image
         for( kiJ = 0; kiJ < aoImg.VdVecAdj.Length; kiJ++ )
         {
            koRec.VdVec[ kiJ ] = 0.0;
            for( kiI = 0; kiI < this.viCountEF; kiI++ )
            {
               koRec.VdVec[ kiJ ] += aoImg.VdVecFSV[ kiI ] * this.voEF[ kiI ].VdData[ kiJ ];
            }
         }

         /// -# Normalize the reconstructed image to 255 range
         kdMax = ( from kdN in koRec.VdVec select kdN ).Max( );
         kdMin = ( from kdN in koRec.VdVec select kdN ).Min( );
         kdDlt = kdMax - kdMin;
         for( kiI = 0; kiI < aoImg.VdVecAdj.Length; kiI++ )
         {
            koRec.VdVec[ kiI ] -= kdMin;
            koRec.VdVec[ kiI ] /= kdDlt;
            koRec.VdVec[ kiI ] *= 255.0;
            if( koRec.VdVec[ kiI ] < 0 )
            {
               koRec.VdVec[ kiI ] = 0;
            }
            if( koRec.VdVec[ kiI ] > 255.0 )
            {
               koRec.VdVec[ kiI ] = 255.0;
            }
         }

         /// -# Calculate the reconstructed error
         kdRecErr = 0.0;
         for( kiI = 0; kiI < aoImg.VdVecAdj.Length; kiI++ )
         {
            kdRecErr += ( aoImg.VdVec[ kiI ] - koRec.VdVecAdj[ kiI ] ) * ( aoImg.VdVec[ kiI ] - koRec.VdVecAdj[ kiI ] );
         }
         kdRecErr = Math.Sqrt( kdRecErr );

         /// -# Find the best match
         koMatches = new TcMatch[ this.voImages.Count ];
         for( kiI = 0; kiI < this.voImages.Count; kiI++ )
         {
            kdDist = 0;
            for( kiJ = 0; kiJ < this.viCountEF; kiJ++ )
            {
               kdDist += ( ( this.voImages[ kiI ].VdVecFSV[ kiJ ] - aoImg.VdVecFSV[ kiJ ] ) *
                           ( this.voImages[ kiI ].VdVecFSV[ kiJ ] - aoImg.VdVecFSV[ kiJ ] ) );
            }
            kdDist = Math.Sqrt( kdDist );
            koMatches[ kiI ] = new TcMatch( this.voImages[ kiI ], kdDist, kiI );
         }

         Array.Sort( koMatches );
         aoMatches = koMatches;

         return( koRec );
      }

      private void mComputeAvgImg( )
      {
         int kiI;
         
         /// -# Allocate memory for the Average Image data
         this.voImgAvg = new TcImage( this.voImages[ 0 ].ViWidth, this.voImages[ 0 ].ViHeight );

         /// -# Sum the images into the average image
         foreach( TcImage koImg in this.voImages )
         {
            for( kiI = 0; kiI < this.voImgAvg.VdVec.Length; kiI++ )
            {
               this.voImgAvg.VdVec[ kiI ] += koImg.VdVec[ kiI ];
            }
         }

         /// -# Divide the sum by the total number of images
         for( kiI = 0; kiI < this.voImgAvg.VdVec.Length; kiI++ )
         {
            this.voImgAvg.VdVec[ kiI ] /= this.voImages.Count;
         }

         /// -# Update the bitmap with the average values
         this.voImgAvg.MUpdateBitmap( );
      }

      private void mAdjustImages( )
      {
         foreach( TcImage koImg in this.voImages )
         {
            this.MAdjustImage( koImg );
         }
      }
   
      private void mInitMatAdj( )
      {
         TcImage koImg;
         int     kiWidth  = this.voImgAvg.ViWidth;
         int     kiHeight = this.voImgAvg.ViHeight;
         int     kiR, kiC;

         /// -# Create NxM Matrix, where N is the size of a linearized image vector and
         ///    M is the number of images
         this.voMatAdj = new Matrix( kiWidth * kiHeight, this.voImages.Count );
         for( kiC = 0; kiC < this.voMatAdj.Columns; kiC++ )
         {
            koImg = this.voImages[ kiC ];
            for( kiR = 0; kiR < this.voMatAdj.Rows; kiR++ )
            {
               this.voMatAdj[ kiR, kiC ] = koImg.VdVecAdj[ kiR ];
            }
         }
      }

      private void mComputeEV( )
      {
         IEigenvalueDecomposition koEDecomp = this.voMatCov.GetEigenvalueDecomposition( );
         IMatrix   koEMat   = koEDecomp.EigenvectorMatrix;
         int       kiCount  = this.voImages.Count;
         int       kiR, kiC;
         double[ ] kdEVal   = koEDecomp.RealEigenvalues;
         double[ ] kdEVec   = new double[ kiCount ];

         this.voEV = new List< TcVector >( );

         for( kiC = 0; kiC < kiCount; kiC++ )
         {
            for( kiR = 0; kiR < kiCount; kiR++ )
            {
               kdEVec[ kiR ] = koEMat[ kiR, kiC ];
            }
            this.voEV.Add( new TcVector( kdEVal[ kiC ], kdEVec, kiCount ) );
         }

         this.voEV.Sort( );
      }

      private void mComputeEF( )
      {
         int    kiCountEF = this.viCountEF;
         int    kiR, kiC;
         double kdSum;
         Matrix koEV;
         TcFace koEF;

         this.voEF = new List< TcFace >( );
         
         /// -# Determine the number of Eigen Faces (Maximum is number of images)
         if( this.voImages.Count < kiCountEF )
         {
            kiCountEF = this.voImages.Count;
         }

         /// -# Create the Eigen Face Matrix and copy Eigen Vectors into it
         this.voMatEF = new Matrix( this.voImgAvg.ViWidth * this.voImgAvg.ViHeight, kiCountEF );
         koEV = new Matrix( this.voImages.Count, kiCountEF );
         for( kiC = 0; kiC < kiCountEF; kiC++ )
         {
            for( kiR = 0; kiR < this.voImages.Count; kiR++ )
            {
               koEV[ kiR, kiC ] = this.voEV[ kiC ].VdData[ kiR ];
            }
         }
         this.voMatEF = ( Matrix )this.voMatAdj.Multiply( koEV );

         /// -# Normalize Eigen Face
         for( kiC = 0; kiC < kiCountEF; kiC++ )
         {
            kdSum = 0.0;
            for( kiR = 0; kiR < this.voMatEF.Rows; kiR++ )
            {
               kdSum += this.voMatEF[ kiR, kiC ] * this.voMatEF[ kiR, kiC ];
            }

            for( kiR = 0; kiR < this.voMatEF.Rows; kiR++ )
            {
               this.voMatEF[ kiR, kiC ] = this.voMatEF[ kiR, kiC ] / Math.Sqrt( kdSum );
            }
         }

         /// -# Copy Eigen Faces to a list
         for( kiC = 0; kiC < kiCountEF; kiC++ )
         {
            koEF = new TcFace( this.voMatEF.Rows );
            for( kiR = 0; kiR < this.voMatEF.Rows; kiR++ )
            {
               koEF.VdData[ kiR ] = this.voMatEF[ kiR, kiC ];
            }
            this.voEF.Add( koEF );
         }
      }

      private void mComputeFS( )
      {
         Matrix koProj = ( Matrix )this.voMatAdj.Transpose( ).Multiply( this.voMatEF );
         int    kiR, kiC;

         for( kiR = 0; kiR < this.voImages.Count; kiR++ )
         {
            for( kiC = 0; kiC < this.viCountEF; kiC++ )
            {
               this.voImages[ kiR ].VdVecFSV[ kiC ] = koProj[ kiR, kiC ];
            }
         }
      }

      private void mComputeFS( TcImage aoImg )
      {
         int    kiI, kiJ;
         double kdSum;
         TcFace koFace;

         for( kiI = 0; kiI < this.voEF.Count; kiI++ )
         {
            kdSum = 0.0;
            koFace = this.voEF[ kiI ];
            for( kiJ = 0; kiJ < this.voEF[ 0 ].VdData.Length; kiJ++ )
            {
               kdSum += aoImg.VdVecAdj[ kiJ ] * koFace.VdData[ kiJ ];
            }
            aoImg.VdVecFSV[ kiI ] = kdSum;
         }
      }
   }
}
