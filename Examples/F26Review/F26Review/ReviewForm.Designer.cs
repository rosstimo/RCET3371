namespace F26Review
{
    partial class ReviewForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ExitButton = new Button();
            DrawPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)DrawPictureBox).BeginInit();
            SuspendLayout();
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(264, 375);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(75, 63);
            ExitButton.TabIndex = 0;
            ExitButton.Text = "E&xit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // DrawPictureBox
            // 
            DrawPictureBox.Location = new Point(12, 32);
            DrawPictureBox.Name = "DrawPictureBox";
            DrawPictureBox.Size = new Size(327, 324);
            DrawPictureBox.TabIndex = 1;
            DrawPictureBox.TabStop = false;
            DrawPictureBox.Click += DrawPictureBox_Click;
            // 
            // ReviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 455);
            Controls.Add(DrawPictureBox);
            Controls.Add(ExitButton);
            Name = "ReviewForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)DrawPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ExitButton;
        private PictureBox DrawPictureBox;
    }
}
