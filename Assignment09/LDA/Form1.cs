using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using FaceRecogPCA;

namespace LDA
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();

         this.MTest( );
      }

      public void MTest( )
      {
         string koPath = @"..\..\..\..\ATTFaceDataSet\Training";

         Dictionary< string, TcClass > koClass = new Dictionary< string, TcClass >( );
         FileInfo                      koInfo;
         string                        koId = "";
         int                           kiI;
         Matrix                        koSw; /**< Within-class Scatter Matrix */
         Matrix                        koSm; /**< Individual class Mean Scatter Matrix */
         Matrix                        koSt; /**< Temporary Scatter Matrix */

         foreach( string koFile in Directory.EnumerateFiles( koPath ) )
         {
            koInfo = new FileInfo( koFile );
            koId = koInfo.Name.Substring( 0, 3 );
            if( !koClass.ContainsKey( koId ) )
            {
               koClass.Add( koId, new TcClass( koId ) );
            }

            koClass[ koId ].Add( new TcSampleImage( koInfo.FullName ) );
         }

         koSw = new Matrix( koClass[ koId ][ 0 ].ViLength, koClass[koId][0].ViLength );

         foreach( TcClass koC in koClass.Values )
         {
            koC.MCalculateMeanVectors( );

            Console.Write( "Mean Vector Class {0}: [", koC.VoId );
            foreach( double kdVal in koC.VdMean )
            {
               Console.Write( kdVal + ", " );
            }
            Console.WriteLine( );

            koSm = new Matrix( koC[ 0 ].ViLength, 1 );
            koSm = koC.MGetMean( );
            for( kiI = 0; kiI < koC.Count; kiI++ )
            {
               koSt = ( Matrix )koC.MGetSample( kiI ).Subtraction( koSm );
               koSt = ( Matrix )koSt.Multiply( koSt.Transpose( ) );
               koSw = ( Matrix )koSw.Addition( koSt );
            }

         }
      }
   }
}
