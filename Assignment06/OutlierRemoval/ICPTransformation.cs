using System.Collections.Generic;
using System.Drawing;

using Mapack;

namespace OutlierRemoval
{
   class ICPTransformation     
   { 
      public static Transformation ComputeTransformation(List<Point> Shp1, List<Point> Shp2)      
      {
         Matrix A = new Matrix(4, 4);
         Matrix B = new Matrix(4, 1);    
         for (int i = 0; i < Shp1.Count; i++) 
         {
            A[0, 0] += 2 * Shp2[i].X * Shp2[i].X + 2 * Shp2[i].Y * Shp2[i].Y;   // provide the code for remaining A matrix entries …                 
            A[0, 1] += 0.0;
            A[0, 2] += 2 * Shp2[i].X;
            A[0, 3] += 2 * Shp2[i].Y;

            A[ 1, 0 ] += 0.0;
            A[ 1, 1 ] += ( 2 * Shp2[i].X * Shp2[i].X ) + ( 2 * Shp2[i].Y * Shp2[i].Y );
            A[ 1, 2 ] +=  2 * Shp2[i].Y;
            A[ 1, 3 ] += -2 * Shp2[i].X;
            
            A[ 2, 0 ] += 2 * Shp2[i].X;
            A[ 2, 1 ] += 2 * Shp2[i].Y;
            A[ 2, 2 ] += 2;
            A[ 2, 3 ] += 0;
            
            A[ 3, 0 ] += 2 * Shp2[i].Y;
            A[ 3, 1 ] += -2 * Shp2[i].X;
            A[ 3, 2 ] += 0;
            A[3, 3] += 2; // TODO: Should this be +2?
 
            B[0, 0] += 2 * Shp1[i].X * Shp2[i].X + 2 * Shp1[i].Y * Shp2[i].Y;   // provide the code for remaining B matrix entries … 
            B[ 1, 0 ] += ( 2 * Shp1[i].X * Shp2[i].Y ) - ( 2 * Shp2[i].X * Shp1[i].Y );
            B[ 2, 0 ] += 2 * Shp1[i].X;
            B[3, 0] += 2 * Shp1[i].Y;                              
         }
         
         Matrix Ainv = A.Inverse;             
         Matrix Res = Ainv * B;             
         Transformation T = new Transformation();             
         T.VdA = Res[0, 0];            
         T.VdB = Res[1, 0];            
         T.VdT1 = Res[2, 0];            
         T.VdT2 = Res[3, 0];           
         
         return T;         
      } 

      public static double ComputeCost(List<Point> P1List, List<Point> P2List, Transformation T)      
      { 
         double cost = 0;             
         for (int i = 0; i < P1List.Count; i++)             
         {                 
            double xprime = T.VdA * P2List[i].X + T.VdB * P2List[i].Y + T.VdT1;                
            double yprime = -1 * T.VdB * P2List[i].X + T.VdA * P2List[i].Y + T.VdT2;                 
            cost += (P1List[i].X - xprime) * (P1List[i].X - xprime) +                     
            (P1List[i].Y - yprime) * (P1List[i].Y - yprime);            
         }             
         return cost;     
      }     
   }
}
