namespace QBoardTesting
{
    partial class QBoardForm
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
            this.components = new System.ComponentModel.Container();
            this.SerialPort = new System.IO.Ports.SerialPort(this.components);
            this.ExitButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(688, 365);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(100, 73);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "E&xit";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(476, 365);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 73);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "&Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(582, 365);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(100, 73);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "&Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // QBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ExitButton);
            this.Name = "QBoardForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort SerialPort;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button SendButton;
    }
}

