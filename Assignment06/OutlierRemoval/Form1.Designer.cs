namespace OutlierRemoval
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
         this.voBtnInitializeShapes = new System.Windows.Forms.Button();
         this.voBtnApplyTransformation = new System.Windows.Forms.Button();
         this.voBtnORIter = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.panel2 = new System.Windows.Forms.Panel();
         this.panel3 = new System.Windows.Forms.Panel();
         this.vnBtnORRansac = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // voBtnInitializeShapes
         // 
         this.voBtnInitializeShapes.Location = new System.Drawing.Point(106, 541);
         this.voBtnInitializeShapes.Name = "voBtnInitializeShapes";
         this.voBtnInitializeShapes.Size = new System.Drawing.Size(152, 31);
         this.voBtnInitializeShapes.TabIndex = 0;
         this.voBtnInitializeShapes.Text = "Initialize Shapes";
         this.voBtnInitializeShapes.UseVisualStyleBackColor = true;
         this.voBtnInitializeShapes.Click += new System.EventHandler(this.voBtnInitializeShapes_Click);
         // 
         // voBtnApplyTransformation
         // 
         this.voBtnApplyTransformation.Location = new System.Drawing.Point(527, 541);
         this.voBtnApplyTransformation.Name = "voBtnApplyTransformation";
         this.voBtnApplyTransformation.Size = new System.Drawing.Size(202, 31);
         this.voBtnApplyTransformation.TabIndex = 1;
         this.voBtnApplyTransformation.Text = "Apply Transformation";
         this.voBtnApplyTransformation.UseVisualStyleBackColor = true;
         this.voBtnApplyTransformation.Click += new System.EventHandler(this.voBtnApplyTransformation_Click);
         // 
         // voBtnORIter
         // 
         this.voBtnORIter.Location = new System.Drawing.Point(941, 541);
         this.voBtnORIter.Name = "voBtnORIter";
         this.voBtnORIter.Size = new System.Drawing.Size(216, 31);
         this.voBtnORIter.TabIndex = 2;
         this.voBtnORIter.Text = "Outlier Removal (Iterative)";
         this.voBtnORIter.UseVisualStyleBackColor = true;
         this.voBtnORIter.Click += new System.EventHandler(this.voBtnORIter_Click);
         // 
         // panel1
         // 
         this.panel1.Location = new System.Drawing.Point(12, 10);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(400, 525);
         this.panel1.TabIndex = 3;
         // 
         // panel2
         // 
         this.panel2.Location = new System.Drawing.Point(418, 10);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(400, 525);
         this.panel2.TabIndex = 4;
         // 
         // panel3
         // 
         this.panel3.Location = new System.Drawing.Point(824, 10);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(400, 525);
         this.panel3.TabIndex = 5;
         // 
         // vnBtnORRansac
         // 
         this.vnBtnORRansac.Location = new System.Drawing.Point(941, 578);
         this.vnBtnORRansac.Name = "vnBtnORRansac";
         this.vnBtnORRansac.Size = new System.Drawing.Size(216, 31);
         this.vnBtnORRansac.TabIndex = 6;
         this.vnBtnORRansac.Text = "Outlier Removal (RANSAC)";
         this.vnBtnORRansac.UseVisualStyleBackColor = true;
         this.vnBtnORRansac.Click += new System.EventHandler(this.vnBtnORRansac_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1242, 624);
         this.Controls.Add(this.vnBtnORRansac);
         this.Controls.Add(this.panel3);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.voBtnORIter);
         this.Controls.Add(this.voBtnApplyTransformation);
         this.Controls.Add(this.voBtnInitializeShapes);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button voBtnInitializeShapes;
      private System.Windows.Forms.Button voBtnApplyTransformation;
      private System.Windows.Forms.Button voBtnORIter;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Button vnBtnORRansac;
   }
}

