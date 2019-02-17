namespace Problem1
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.voBtnConvolve = new System.Windows.Forms.Button();
         this.voPicLeft = new System.Windows.Forms.PictureBox();
         this.voPicRight = new System.Windows.Forms.PictureBox();
         this.voBtnBrowse = new System.Windows.Forms.Button();
         this.voRBtnIdentity = new System.Windows.Forms.RadioButton();
         this.voRBtnAverage = new System.Windows.Forms.RadioButton();
         this.voRBtnHighPass = new System.Windows.Forms.RadioButton();
         this.voRBtnSharpen = new System.Windows.Forms.RadioButton();
         this.voRBtnGaussian = new System.Windows.Forms.RadioButton();
         this.voRBtnGradient = new System.Windows.Forms.RadioButton();
         this.voRBtnLaplacian = new System.Windows.Forms.RadioButton();
         this.voGBKernels = new System.Windows.Forms.GroupBox();
         this.voRBtnDiffGaussian = new System.Windows.Forms.RadioButton();
         this.voRBtnCustom = new System.Windows.Forms.RadioButton();
         ((System.ComponentModel.ISupportInitialize)(this.voPicLeft)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.voPicRight)).BeginInit();
         this.voGBKernels.SuspendLayout();
         this.SuspendLayout();
         // 
         // voBtnConvolve
         // 
         this.voBtnConvolve.Location = new System.Drawing.Point(14, 52);
         this.voBtnConvolve.Name = "voBtnConvolve";
         this.voBtnConvolve.Size = new System.Drawing.Size(174, 29);
         this.voBtnConvolve.TabIndex = 1;
         this.voBtnConvolve.Text = "Convolve";
         this.voBtnConvolve.UseVisualStyleBackColor = true;
         this.voBtnConvolve.Click += new System.EventHandler(this.mBtnConvolve_Click);
         // 
         // voPicLeft
         // 
         this.voPicLeft.Location = new System.Drawing.Point(218, 15);
         this.voPicLeft.Name = "voPicLeft";
         this.voPicLeft.Size = new System.Drawing.Size(627, 780);
         this.voPicLeft.TabIndex = 1;
         this.voPicLeft.TabStop = false;
         // 
         // voPicRight
         // 
         this.voPicRight.Location = new System.Drawing.Point(866, 15);
         this.voPicRight.Name = "voPicRight";
         this.voPicRight.Size = new System.Drawing.Size(627, 780);
         this.voPicRight.TabIndex = 2;
         this.voPicRight.TabStop = false;
         // 
         // voBtnBrowse
         // 
         this.voBtnBrowse.Location = new System.Drawing.Point(14, 17);
         this.voBtnBrowse.Name = "voBtnBrowse";
         this.voBtnBrowse.Size = new System.Drawing.Size(174, 29);
         this.voBtnBrowse.TabIndex = 0;
         this.voBtnBrowse.Text = "Browse";
         this.voBtnBrowse.UseVisualStyleBackColor = true;
         this.voBtnBrowse.Click += new System.EventHandler(this.mBtnBrowse_Click);
         // 
         // voRBtnIdentity
         // 
         this.voRBtnIdentity.AutoSize = true;
         this.voRBtnIdentity.Location = new System.Drawing.Point(4, 29);
         this.voRBtnIdentity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnIdentity.Name = "voRBtnIdentity";
         this.voRBtnIdentity.Size = new System.Drawing.Size(86, 24);
         this.voRBtnIdentity.TabIndex = 2;
         this.voRBtnIdentity.TabStop = true;
         this.voRBtnIdentity.Text = "Identity";
         this.voRBtnIdentity.UseVisualStyleBackColor = true;
         this.voRBtnIdentity.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnAverage
         // 
         this.voRBtnAverage.AutoSize = true;
         this.voRBtnAverage.Location = new System.Drawing.Point(4, 65);
         this.voRBtnAverage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnAverage.Name = "voRBtnAverage";
         this.voRBtnAverage.Size = new System.Drawing.Size(175, 24);
         this.voRBtnAverage.TabIndex = 3;
         this.voRBtnAverage.TabStop = true;
         this.voRBtnAverage.Text = "Average (Low Pass)";
         this.voRBtnAverage.UseVisualStyleBackColor = true;
         this.voRBtnAverage.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnHighPass
         // 
         this.voRBtnHighPass.AutoSize = true;
         this.voRBtnHighPass.Location = new System.Drawing.Point(4, 100);
         this.voRBtnHighPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnHighPass.Name = "voRBtnHighPass";
         this.voRBtnHighPass.Size = new System.Drawing.Size(106, 24);
         this.voRBtnHighPass.TabIndex = 4;
         this.voRBtnHighPass.TabStop = true;
         this.voRBtnHighPass.Text = "High Pass";
         this.voRBtnHighPass.UseVisualStyleBackColor = true;
         this.voRBtnHighPass.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnSharpen
         // 
         this.voRBtnSharpen.AutoSize = true;
         this.voRBtnSharpen.Location = new System.Drawing.Point(4, 135);
         this.voRBtnSharpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnSharpen.Name = "voRBtnSharpen";
         this.voRBtnSharpen.Size = new System.Drawing.Size(116, 24);
         this.voRBtnSharpen.TabIndex = 5;
         this.voRBtnSharpen.TabStop = true;
         this.voRBtnSharpen.Text = "Sharpening";
         this.voRBtnSharpen.UseVisualStyleBackColor = true;
         this.voRBtnSharpen.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnGaussian
         // 
         this.voRBtnGaussian.AutoSize = true;
         this.voRBtnGaussian.Location = new System.Drawing.Point(4, 171);
         this.voRBtnGaussian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnGaussian.Name = "voRBtnGaussian";
         this.voRBtnGaussian.Size = new System.Drawing.Size(102, 24);
         this.voRBtnGaussian.TabIndex = 6;
         this.voRBtnGaussian.TabStop = true;
         this.voRBtnGaussian.Text = "Gaussian";
         this.voRBtnGaussian.UseVisualStyleBackColor = true;
         this.voRBtnGaussian.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnGradient
         // 
         this.voRBtnGradient.AutoSize = true;
         this.voRBtnGradient.Location = new System.Drawing.Point(4, 206);
         this.voRBtnGradient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnGradient.Name = "voRBtnGradient";
         this.voRBtnGradient.Size = new System.Drawing.Size(168, 24);
         this.voRBtnGradient.TabIndex = 7;
         this.voRBtnGradient.TabStop = true;
         this.voRBtnGradient.Text = "F. Deriv. (Gradient)";
         this.voRBtnGradient.UseVisualStyleBackColor = true;
         this.voRBtnGradient.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnLaplacian
         // 
         this.voRBtnLaplacian.AutoSize = true;
         this.voRBtnLaplacian.Location = new System.Drawing.Point(4, 242);
         this.voRBtnLaplacian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnLaplacian.Name = "voRBtnLaplacian";
         this.voRBtnLaplacian.Size = new System.Drawing.Size(175, 24);
         this.voRBtnLaplacian.TabIndex = 8;
         this.voRBtnLaplacian.TabStop = true;
         this.voRBtnLaplacian.Text = "S. Deriv. (Laplacian)";
         this.voRBtnLaplacian.UseVisualStyleBackColor = true;
         this.voRBtnLaplacian.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voGBKernels
         // 
         this.voGBKernels.Controls.Add(this.voRBtnCustom);
         this.voGBKernels.Controls.Add(this.voRBtnDiffGaussian);
         this.voGBKernels.Controls.Add(this.voRBtnIdentity);
         this.voGBKernels.Controls.Add(this.voRBtnLaplacian);
         this.voGBKernels.Controls.Add(this.voRBtnAverage);
         this.voGBKernels.Controls.Add(this.voRBtnGradient);
         this.voGBKernels.Controls.Add(this.voRBtnHighPass);
         this.voGBKernels.Controls.Add(this.voRBtnGaussian);
         this.voGBKernels.Controls.Add(this.voRBtnSharpen);
         this.voGBKernels.Location = new System.Drawing.Point(14, 89);
         this.voGBKernels.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voGBKernels.Name = "voGBKernels";
         this.voGBKernels.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voGBKernels.Size = new System.Drawing.Size(196, 360);
         this.voGBKernels.TabIndex = 10;
         this.voGBKernels.TabStop = false;
         this.voGBKernels.Text = "Kernels";
         // 
         // voRBtnDiffGaussian
         // 
         this.voRBtnDiffGaussian.AutoSize = true;
         this.voRBtnDiffGaussian.Location = new System.Drawing.Point(4, 277);
         this.voRBtnDiffGaussian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnDiffGaussian.Name = "voRBtnDiffGaussian";
         this.voRBtnDiffGaussian.Size = new System.Drawing.Size(161, 24);
         this.voRBtnDiffGaussian.TabIndex = 9;
         this.voRBtnDiffGaussian.TabStop = true;
         this.voRBtnDiffGaussian.Text = "Diff. of Gaussians";
         this.voRBtnDiffGaussian.UseVisualStyleBackColor = true;
         this.voRBtnDiffGaussian.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // voRBtnCustom
         // 
         this.voRBtnCustom.AutoSize = true;
         this.voRBtnCustom.Location = new System.Drawing.Point(4, 311);
         this.voRBtnCustom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.voRBtnCustom.Name = "voRBtnCustom";
         this.voRBtnCustom.Size = new System.Drawing.Size(89, 24);
         this.voRBtnCustom.TabIndex = 10;
         this.voRBtnCustom.TabStop = true;
         this.voRBtnCustom.Text = "Custom";
         this.voRBtnCustom.UseVisualStyleBackColor = true;
         this.voRBtnCustom.CheckedChanged += new System.EventHandler(this.mKernelCheckedChanged);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1536, 854);
         this.Controls.Add(this.voGBKernels);
         this.Controls.Add(this.voBtnBrowse);
         this.Controls.Add(this.voPicRight);
         this.Controls.Add(this.voPicLeft);
         this.Controls.Add(this.voBtnConvolve);
         this.Name = "Form1";
         this.Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)(this.voPicLeft)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.voPicRight)).EndInit();
         this.voGBKernels.ResumeLayout(false);
         this.voGBKernels.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button voBtnConvolve;
      private System.Windows.Forms.PictureBox voPicLeft;
      private System.Windows.Forms.PictureBox voPicRight;
      private System.Windows.Forms.Button voBtnBrowse;
      private System.Windows.Forms.RadioButton voRBtnIdentity;
      private System.Windows.Forms.RadioButton voRBtnAverage;
      private System.Windows.Forms.RadioButton voRBtnHighPass;
      private System.Windows.Forms.RadioButton voRBtnSharpen;
      private System.Windows.Forms.RadioButton voRBtnGaussian;
      private System.Windows.Forms.RadioButton voRBtnGradient;
      private System.Windows.Forms.RadioButton voRBtnLaplacian;
      private System.Windows.Forms.GroupBox voGBKernels;
      private System.Windows.Forms.RadioButton voRBtnDiffGaussian;
      private System.Windows.Forms.RadioButton voRBtnCustom;
   }
}

