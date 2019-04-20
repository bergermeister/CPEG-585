using System;

namespace LDA
{
   public class TcMatch : IComparable
   {
      private int     viImgNum;
      private string  voImgName;
      private TcImage voImg;
      private double  vdDistance;    /**< Eucledian Distance */

      public int     ViImgNum  { get{ return( this.viImgNum ); } }
      public string  VoImgName { get{ return( this.voImgName ); } }
      public TcImage VoImg     { get{ return( this.voImg ); } }
      public double  VdDistance{ get{ return( this.vdDistance ); } }

      public TcMatch( TcImage aoImg, double kdError, int kiImgNum )
      {
         this.viImgNum   = kiImgNum;
         this.voImgName  = aoImg.VoName;
         this.voImg      = aoImg;
         this.vdDistance = kdError;
      }

      public int CompareTo( Object aoRHS )
      {
         TcMatch koMatch = aoRHS as TcMatch;
         return( this.vdDistance.CompareTo( koMatch.vdDistance ) );
      }
   }
}
