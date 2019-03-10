using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlierRemoval
{
   public partial class Form1 : Form
   {
      private Random        voRand   = new Random( ( int )DateTime.Now.ToBinary( ) );
      private List< Point > voShape1 = new List< Point >( );
      private List< Point > voShape2 = new List< Point >( );

      public Form1()
      {
         InitializeComponent();
      }

      private void voBtnInitializeShapes_Click(object sender, EventArgs e)
      {
         Point p1a = new Point(20, 30);  
         Point p2a = new Point(120, 50);             
         Point p3a = new Point(160, 80);             
         Point p4a = new Point(180, 300);             
         Point p5a = new Point(100, 220);             
         Point p6a = new Point(50, 280);             
         Point p7a = new Point(20, 140); 

         this.voShape1.Clear( );
         this.voShape2.Clear( );
 
         this.voShape1.Add(p1a);             
         this.voShape1.Add(p2a);             
         this.voShape1.Add(p3a);             
         this.voShape1.Add(p4a);             
         this.voShape1.Add(p5a);             
         this.voShape1.Add(p6a); 
         this.voShape1.Add(p7a); 
 
         Transformation T2 = new Transformation();  
         T2.VdA = 1.05; T2.VdB = 0.05; T2.VdT1 = 15; T2.VdT2 = 22;
         this.voShape2 = T2.MApply( this.voShape1 );
         this.voShape2[ 2 ] = new Point( this.voShape2[ 2 ].X + 10, this.voShape2[ 2 ].Y + 3 );
         // change one point             
         // add outliers to both shapes             
         Point ptOutlier1 = new Point(200, 230);             
         this.voShape1.Add( ptOutlier1 );             
         Point ptOutLier2 = new Point(270, 160);             
         this.voShape2.Add(ptOutLier2); 
         Pen pBlue = new Pen(Brushes.Blue, 1);             
         Pen pRed = new Pen(Brushes.Red, 1);            
         Graphics g = this.panel1.CreateGraphics();             
         this.mDisplayShape( this.voShape1, pBlue, g );            
         this.mDisplayShape( this.voShape2, pRed, g ); 
      }
      
      
      private void mDisplayShape( List< Point > aoShape, Pen aoPen, Graphics aoGraphics )
      {
         Point? koPrev = null; // nullable

         foreach( Point koPt in aoShape )
         {
            aoGraphics.DrawEllipse( aoPen, new Rectangle( koPt.X - 2, koPt.Y - 2, 4, 4 ) );
            if( koPrev != null )
            {
               aoGraphics.DrawLine( aoPen, ( Point )koPrev, koPt );
            }
            koPrev = koPt;
         }
         aoGraphics.DrawLine( aoPen, aoShape[ 0 ], aoShape[ aoShape.Count -1 ] );
      }

      private void voBtnApplyTransformation_Click(object sender, EventArgs e)
      {             
         Transformation T = ICPTransformation.ComputeTransformation(this.voShape1, this.voShape2);             
         MessageBox.Show("Cost = " + ICPTransformation.ComputeCost(this.voShape1, this.voShape2, T).ToString());             
         List<Point> Shape2T = T.MApply(this.voShape2);             
         Pen pBlue = new Pen(Brushes.Blue, 1);             
         Pen pRed = new Pen(Brushes.Red, 1);             
         Graphics g = this.panel2.CreateGraphics();             
         g.Clear( this.BackColor );
         this.mDisplayShape( this.voShape1, pBlue, g );             
         this.mDisplayShape( Shape2T, pRed, g ); 
      }

      private void voBtnORIter_Click(object sender, EventArgs e)
      {
         List< Point >  koShape1 = new List< Point >( this.voShape1 );
         List< Point >  koShape2 = new List< Point >( this.voShape2 );
         int            kiCount = this.voShape1.Count;
         int            kiLoop;
         int            kiIdx;
         Point          koPt1;
         Point          koPt2;
         Transformation koTMin = ICPTransformation.ComputeTransformation( koShape1, koShape2 ); 
         Transformation koT;
         double         kdCostMin = ICPTransformation.ComputeCost( koShape1, koShape2, koTMin );
         double         kdCost;

         kiIdx = 0;
         for( kiLoop = 0; kiLoop < kiCount; kiLoop++ )
         { 
            koPt1 = this.voShape1[ kiIdx ];
            koPt2 = this.voShape2[ kiIdx ];

            this.voShape1.RemoveAt( kiIdx );
            this.voShape2.RemoveAt( kiIdx );

            koT    = ICPTransformation.ComputeTransformation( this.voShape1, this.voShape2 );             
            kdCost = ICPTransformation.ComputeCost( this.voShape1, this.voShape2, koT );

            // If the cost improved
            if( kdCost < kdCostMin )
            {
               kdCostMin = kdCost;
               koTMin    = koT;
               koShape1 = new List< Point >( this.voShape1 );
               koShape2 = new List< Point >( this.voShape2 );
            }

            // Put the points back
            this.voShape1.Insert( kiIdx, koPt1 );
            this.voShape2.Insert( kiIdx, koPt2 );
            kiIdx++;
         }
         MessageBox.Show("Cost = " + kdCostMin.ToString( ) );             

         List<Point> Shape2T = koTMin.MApply( koShape2 );             
         Pen pBlue = new Pen(Brushes.Blue, 1);             
         Pen pRed = new Pen(Brushes.Red, 1);             
         Graphics g = this.panel3.CreateGraphics();      
         g.Clear( this.BackColor );
         this.mDisplayShape( koShape1, pBlue, g );             
         this.mDisplayShape( Shape2T, pRed, g ); 
      }

      private void vnBtnORRansac_Click(object sender, EventArgs e)
      {
         Transformation koT;
         SortedSet< int > koIndices;
         List< Point >  koImg1;
         List< Point >  koImg2;
         double         kdError;

         this.mRANSAC( this.voShape2, this.voShape1, 2, 10, 100, this.voShape1.Count - 2, out koT, out koIndices, out kdError );

         koImg1 = this.mGetPoints( this.voShape1, koIndices );
         koImg2 = this.mGetPoints( this.voShape2, koIndices );

         MessageBox.Show("Cost = " + kdError.ToString( ) );

         List<Point> Shape2T = koT.MApply( koImg2 );             
         Pen pBlue = new Pen(Brushes.Blue, 1);             
         Pen pRed = new Pen(Brushes.Red, 1);             
         Graphics g = this.panel3.CreateGraphics();             
         g.Clear( this.BackColor );
         this.mDisplayShape( koImg1, pBlue, g );             
         this.mDisplayShape( Shape2T, pRed, g ); 
      }

      /**
       * @param aoData     A set of observations
       * @param aoModel    A model that can be fitted to data
       * @param aiN        The minimum number of data required to fit the model
       * @param aiK        The number of iterations performed by the algorithm
       * @param adT        A threshold value for determining when a datum fits a model
       * @param aiD        The number of close data values required to assert that a model fits well to data 
       */ 
      private bool mRANSAC( List< Point > aoData, List< Point > aoModel, int aiN, int aiK, double adT, int aiD, 
                            out Transformation aoBestModel, out SortedSet< int > aoBestConsensusSet, out double adBestError )
      {
         bool           kbSuccess    = false;
         int            kiIterations = 0;
         double         kdThisError;
         SortedSet< int > koMaybeInliers;
         SortedSet< int > koConsensusSet;
         List< Point >  koImg1;
         List< Point >  koImg2;
         Transformation koMaybeModel;
         Transformation koBetterModel;

         aoBestModel        = null;
         aoBestConsensusSet = null;
         adBestError        = Double.MaxValue;

         while( kiIterations < aiK )
         { 
            koMaybeInliers = this.mGetRandom( aiN, aoData.Count );
            koImg1         = this.mGetPoints( aoModel, koMaybeInliers );
            koImg2         = this.mGetPoints( aoData,  koMaybeInliers );
            koMaybeModel   = ICPTransformation.ComputeTransformation( koImg1, koImg2 );
            koConsensusSet = new SortedSet< int >( koMaybeInliers );

            // For every point in data 
            for( int kiIdx = 0; kiIdx < aoData.Count; kiIdx++ )
            {
               // not in maybe_inliers
               if( !koMaybeInliers.Contains( kiIdx ) )
               {
                  koImg1.Add( aoModel[ kiIdx ] );
                  koImg2.Add( aoData[ kiIdx ] ); 
                  Transformation koT = ICPTransformation.ComputeTransformation( koImg1, koImg2 );
                  if( ICPTransformation.ComputeCost( koImg1, koImg2, koT ) < adT )
                  {
                     koConsensusSet.Add( kiIdx );
                  }
                  else
                  {
                     koImg1.RemoveAt( koImg1.Count - 1 );
                     koImg2.RemoveAt( koImg2.Count - 1 );
                  }
               }
            }

            // if the number of elements in consensus_set is > d
            if( koConsensusSet.Count > aiD )
            { 
               // This implies that we may have found a good model, now test how good it is
               koImg1        = this.mGetPoints( aoModel, koConsensusSet );
               koImg2        = this.mGetPoints( aoData,  koConsensusSet );
               koBetterModel = ICPTransformation.ComputeTransformation( koImg1, koImg2 );                  
               kdThisError   = ICPTransformation.ComputeCost( koImg1, koImg2, koBetterModel );
               if( kdThisError < adBestError )
               { 
                  // We have found a model which is better than any of the previous ones, keep it until a better one is found
                  aoBestModel        = koBetterModel;
                  aoBestConsensusSet = koConsensusSet;
                  adBestError        = kdThisError;
                  kbSuccess = true;
               }
            }

            kiIterations++; 
         }

         return( kbSuccess );
      }

      private SortedSet< int > mGetRandom( int aiCount, int aiMax )
      {
         SortedSet< int > koIndices = new SortedSet< int >( );
         int           kiIdx;

         while( koIndices.Count < aiCount )
         {
            kiIdx = this.voRand.Next( aiMax );
            if( !koIndices.Contains( kiIdx ) )
            { 
               koIndices.Add( kiIdx );
            }
         }

         return( koIndices );
      }

      private List< Point > mGetPoints( List< Point > aoPoints, SortedSet< int > aoIndices )
      {
         List< Point > koPoints = new List< Point >( aoIndices.Count );

         foreach( int aiIdx in aoIndices )
         {
            koPoints.Add( aoPoints[ aiIdx ] );
         }

         return( koPoints );
      }
   }
}
