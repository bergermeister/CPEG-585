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
      private const string voPath = @"..\..\..\..\ATTFaceDataSet\Training";
      private TcFaceRecog  voFR;

      public Form1()
      {
         InitializeComponent();

         this.voFR = new TcFaceRecog( voPath, 100 );
         this.voFR.MTrain( );
      }
   }
}
