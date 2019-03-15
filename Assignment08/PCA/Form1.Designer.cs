namespace PCA
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
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.button3 = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         this.SuspendLayout();
         // 
         // pictureBox1
         // 
         this.pictureBox1.Location = new System.Drawing.Point(12, 10);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(249, 382);
         this.pictureBox1.TabIndex = 0;
         this.pictureBox1.TabStop = false;
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(531, 349);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(91, 43);
         this.button1.TabIndex = 1;
         this.button1.Text = "Browse";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Location = new System.Drawing.Point(531, 398);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(91, 40);
         this.button2.TabIndex = 2;
         this.button2.Text = "Accuracy";
         this.button2.UseVisualStyleBackColor = true;
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // button3
         // 
         this.button3.Location = new System.Drawing.Point(531, 48);
         this.button3.Name = "button3";
         this.button3.Size = new System.Drawing.Size(91, 43);
         this.button3.TabIndex = 3;
         this.button3.Text = "Initialize";
         this.button3.UseVisualStyleBackColor = true;
         this.button3.Click += new System.EventHandler(this.button3_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(531, 16);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(91, 26);
         this.textBox1.TabIndex = 4;
         // 
         // pictureBox2
         // 
         this.pictureBox2.Location = new System.Drawing.Point(267, 10);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(258, 382);
         this.pictureBox2.TabIndex = 5;
         this.pictureBox2.TabStop = false;
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(218, 412);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(91, 26);
         this.textBox2.TabIndex = 6;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(635, 450);
         this.Controls.Add(this.textBox2);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.button3);
         this.Controls.Add(this.button2);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.pictureBox1);
         this.Name = "Form1";
         this.Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button3;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.TextBox textBox2;
   }
}

