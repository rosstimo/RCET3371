namespace KeyPress
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
            this.lblKeyDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // lblKeyDisplay
            this.lblKeyDisplay.AutoSize = false;
            this.lblKeyDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKeyDisplay.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblKeyDisplay.Location = new System.Drawing.Point(50, 50);
            this.lblKeyDisplay.Name = "lblKeyDisplay";
            this.lblKeyDisplay.Size = new System.Drawing.Size(700, 350);
            this.lblKeyDisplay.TabIndex = 0;
            this.lblKeyDisplay.Text = "Press any key...";
            this.lblKeyDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblKeyDisplay);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Key Press Display";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblKeyDisplay;
    }
}

