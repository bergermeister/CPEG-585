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
         TcFaceRecog koFR = new TcFaceRecog( koPath, 100 );
         koFR.MTrain( );

      }
   }
}
