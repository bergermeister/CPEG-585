namespace LDA.LDA
{
   using System;
   using System.Collections.Generic;
   using FaceRecogPCA;

   public class TcClass : List< TcImage >
   {
      private string    voId;
      private double[ ] vdMean;  /**< Mean Vectors */ 

      public TcClass( string aoId )
      {
         this.voId = aoId;
      }

      public string VoId
      {
         get{ return( this.voId ); }
      }

      public double[ ] VdMean
      {
         get{ return( this.vdMean ); }
      }

      public void MCalculateMeanVector( )
      {
         int kiI;

         /// -# Initialize the mean vector
         this.vdMean = new double[ this[ 0 ].VdVecFSV.Length ];

         /// -# Zero out the mean vector
         for( kiI = 0; kiI < this.vdMean.Length; kiI++ )
         {
            this.vdMean[ kiI ] = 0.0;
         }

         /// -# Add all linearized sample data into the mean vector
         foreach( TcImage koImg in this )
         {
            for( kiI = 0; kiI < koImg.VdVecFSV.Length; kiI++ )
            {
               this.vdMean[ kiI ] += koImg.VdVecFSV[ kiI ];
            }
         }

         /// -# Divide each element in the mean vector by the number of samples
         for( kiI = 0; kiI < this.vdMean.Length; kiI++ )
         {
            this.vdMean[ kiI ] /= this.Count;
         }
      }

      public Matrix MGetMean( )
      {
         Matrix koMat = new Matrix( this.vdMean.Length, 1 );
         int    kiI;

         for( kiI = 0; kiI < this.vdMean.Length; kiI++ )
         {
            koMat[ kiI, 0 ] = this.vdMean[ kiI ];
         }
         
         return( koMat );
      }

      public Matrix MGetSample( int aiIndex )
      {
         Matrix koMat = new Matrix( this[ aiIndex ].VdVecFSV.Length, 1 );
         int    kiI;

         for( kiI = 0; kiI < this[ aiIndex ].VdVecFSV.Length; kiI++ )
         {
            koMat[ kiI, 0 ] = this[ aiIndex ].VdVecFSV[ kiI ];
         }
         
         return( koMat );
      }

      public TcImage MBestMatch( TcImage aoImg, ref double adDist )
      {
         TcImage koBest = null;
         double  kdDist;
         int     kiI;

         adDist = double.MaxValue;

         foreach( TcImage koImg in this )
         {
            kdDist = 0.0;
            for( kiI = 0; kiI < koImg.VdVecLDA.Length; kiI++ )
            {
               kdDist += ( ( koImg.VdVecLDA[ kiI ] - aoImg.VdVecLDA[ kiI ] ) *
                           ( koImg.VdVecLDA[ kiI ] - aoImg.VdVecLDA[ kiI ] ) );
            }
            kdDist = Math.Sqrt( kdDist );

            if( kdDist < adDist )
            {
               koBest = koImg;
               adDist = kdDist;
            }
         }

         return( koBest );
      }
   }
}
