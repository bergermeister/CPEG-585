namespace Problem1
{
    partial class FormShift
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShiftAmount = new System.Windows.Forms.TextBox();
            this.btnShift = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Yellow;
            this.btnCancel.Location = new System.Drawing.Point(343, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Shift Amount (e.g., 20 or -15";
            // 
            // txtShiftAmount
            // 
            this.txtShiftAmount.Location = new System.Drawing.Point(256, 41);
            this.txtShiftAmount.Name = "txtShiftAmount";
            this.txtShiftAmount.Size = new System.Drawing.Size(81, 22);
            this.txtShiftAmount.TabIndex = 13;
            this.txtShiftAmount.Text = "0";
            // 
            // btnShift
            // 
            this.btnShift.BackColor = System.Drawing.Color.Yellow;
            this.btnShift.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnShift.Location = new System.Drawing.Point(343, 41);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(75, 23);
            this.btnShift.TabIndex = 12;
            this.btnShift.Text = "Shift";
            this.btnShift.UseVisualStyleBackColor = false;
            // 
            // FormShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 122);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtShiftAmount);
            this.Controls.Add(this.btnShift);
            this.Name = "FormShift";
            this.Text = "FormShift";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShiftAmount;
        private System.Windows.Forms.Button btnShift;
    }
}