namespace Problem2
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
         this.VoPBOriginal = new System.Windows.Forms.PictureBox();
         this.VoBtnBrowse = new System.Windows.Forms.Button();
         this.VoBtnHE = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.VoPBOriginal)).BeginInit();
         this.SuspendLayout();
         // 
         // VoPBOriginal
         // 
         this.VoPBOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.VoPBOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.VoPBOriginal.Location = new System.Drawing.Point(12, 29);
         this.VoPBOriginal.Name = "VoPBOriginal";
         this.VoPBOriginal.Size = new System.Drawing.Size(754, 441);
         this.VoPBOriginal.TabIndex = 0;
         this.VoPBOriginal.TabStop = false;
         // 
         // VoBtnBrowse
         // 
         this.VoBtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.VoBtnBrowse.Location = new System.Drawing.Point(66, 476);
         this.VoBtnBrowse.Name = "VoBtnBrowse";
         this.VoBtnBrowse.Size = new System.Drawing.Size(120, 56);
         this.VoBtnBrowse.TabIndex = 2;
         this.VoBtnBrowse.Text = "Browse";
         this.VoBtnBrowse.UseVisualStyleBackColor = true;
         this.VoBtnBrowse.Click += new System.EventHandler(this.VoBtnBrowse_Click);
         // 
         // VoBtnHE
         // 
         this.VoBtnHE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.VoBtnHE.Location = new System.Drawing.Point(540, 476);
         this.VoBtnHE.Name = "VoBtnHE";
         this.VoBtnHE.Size = new System.Drawing.Size(120, 56);
         this.VoBtnHE.TabIndex = 3;
         this.VoBtnHE.Text = "Histogram Equalization";
         this.VoBtnHE.UseVisualStyleBackColor = true;
         this.VoBtnHE.Click += new System.EventHandler(this.VoBtnHE_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(778, 544);
         this.Controls.Add(this.VoBtnHE);
         this.Controls.Add(this.VoBtnBrowse);
         this.Controls.Add(this.VoPBOriginal);
         this.Name = "Form1";
         this.Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)(this.VoPBOriginal)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox VoPBOriginal;
      private System.Windows.Forms.Button VoBtnBrowse;
      private System.Windows.Forms.Button VoBtnHE;


   }
}

