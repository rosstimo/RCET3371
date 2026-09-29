namespace SerialExample
{
    partial class SerialForm
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
            button1 = new Button();
            ConnectButton = new Button();
            SuspendLayout();
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(434, 236);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(92, 47);
            ExitButton.TabIndex = 0;
            ExitButton.Text = "E&xit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(336, 236);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(92, 47);
            ConnectButton.TabIndex = 2;
            ConnectButton.Text = "&Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // SerialForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(538, 295);
            Controls.Add(ConnectButton);
            Controls.Add(button1);
            Controls.Add(ExitButton);
            Name = "SerialForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button ExitButton;
        private Button button1;
        private Button ConnectButton;
    }
}
