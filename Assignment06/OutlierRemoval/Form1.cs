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
         Transformation koTMin = ICPTransformation.ComputeTransformation( koShape1, koShape2 );             ;
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
         this.mDisplayShape( koShape1, pBlue, g );             
         this.mDisplayShape( Shape2T, pRed, g ); 
      }

      private void vnBtnORRansac_Click(object sender, EventArgs e)
      {
         /*
         input:     data - a set of observations     
            model - a model that can be fitted to data      
            n - the minimum number of data required to fit the model     
            k - the number of iterations performed by the algorithm     
            t - a threshold value for determining when a datum fits a model     
            d - the number of close data values required to assert that a model fits well to data 
         output:     
            best_model - model parameters which best fit the data (or nil if no good model is found)     
            best_consensus_set - data point from which this model has been estimated     
            best_error - the error of this model relative to the data  
 
         iterations := 0 
         best_model := nil 
         best_consensus_set := nil 
         best_error := infinity 
            
         while iterations < k      
            maybe_inliers := n randomly selected values from data     
            maybe_model := model parameters fitted to maybe_inliers     
            consensus_set := maybe_inliers 
            
            for every point in data not in maybe_inliers          
               if point fits maybe_model with an error smaller than t            
                  add point to consensus_set          
                  
            if the number of elements in consensus_set is > d          
               (this implies that we may have found a good model,         
               now test how good it is)         
               better_model := model parameters fitted to all points in consensus_set         
               this_error := a measure of how well better_model fits these points         
               if this_error < best_error             
                  (we have found a model which is better than any of the previous ones,             
                   keep it until a better one is found)             
                  best_model := better_model             
                  best_consensus_set := consensus_set             
                  best_error := this_error           
            
            increment iterations 
 
         return best_model, best_consensus_set, best_error 
         */
      }
   }
}
