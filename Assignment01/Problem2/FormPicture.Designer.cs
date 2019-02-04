namespace Problem2
{
   partial class FormPicture
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
         this.VoPB = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.VoPB)).BeginInit();
         this.SuspendLayout();
         // 
         // VoPB
         // 
         this.VoPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.VoPB.Location = new System.Drawing.Point(12, 12);
         this.VoPB.Name = "VoPB";
         this.VoPB.Size = new System.Drawing.Size(776, 426);
         this.VoPB.TabIndex = 0;
         this.VoPB.TabStop = false;
         // 
         // FormPicture
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.VoPB);
         this.Name = "FormPicture";
         this.Text = "FormPicture";
         this.Load += new System.EventHandler(this.FormPicture_Load);
         ((System.ComponentModel.ISupportInitialize)(this.VoPB)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox VoPB;
   }
}