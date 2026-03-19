namespace SerialPortExample
{
    partial class SerialPortForm
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.ExitButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PortsComboBox = new System.Windows.Forms.ComboBox();
            this.PortStatusStrip = new System.Windows.Forms.StatusStrip();
            this.PortStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PortStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.RefreshPortsButton = new System.Windows.Forms.Button();
            this.PortStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(652, 321);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(136, 82);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "E&xit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(510, 321);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(136, 82);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "&Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // PortsComboBox
            // 
            this.PortsComboBox.FormattingEnabled = true;
            this.PortsComboBox.Location = new System.Drawing.Point(539, 56);
            this.PortsComboBox.Name = "PortsComboBox";
            this.PortsComboBox.Size = new System.Drawing.Size(121, 28);
            this.PortsComboBox.TabIndex = 2;
            // 
            // PortStatusStrip
            // 
            this.PortStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.PortStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PortStatusLabel});
            this.PortStatusStrip.Location = new System.Drawing.Point(0, 418);
            this.PortStatusStrip.Name = "PortStatusStrip";
            this.PortStatusStrip.Size = new System.Drawing.Size(800, 32);
            this.PortStatusStrip.TabIndex = 3;
            this.PortStatusStrip.Text = "statusStrip1";
            // 
            // PortStatusLabel
            // 
            this.PortStatusLabel.Name = "PortStatusLabel";
            this.PortStatusLabel.Size = new System.Drawing.Size(55, 25);
            this.PortStatusLabel.Text = "None";
            // 
            // PortStatusTimer
            // 
            this.PortStatusTimer.Enabled = true;
            this.PortStatusTimer.Interval = 500;
            this.PortStatusTimer.Tick += new System.EventHandler(this.PortStatusTimer_Tick);
            // 
            // RefreshPortsButton
            // 
            this.RefreshPortsButton.Location = new System.Drawing.Point(666, 56);
            this.RefreshPortsButton.Name = "RefreshPortsButton";
            this.RefreshPortsButton.Size = new System.Drawing.Size(89, 28);
            this.RefreshPortsButton.TabIndex = 4;
            this.RefreshPortsButton.Text = "&Refresh";
            this.RefreshPortsButton.UseVisualStyleBackColor = true;
            this.RefreshPortsButton.Click += new System.EventHandler(this.RefreshPortsButton_Click);
            // 
            // SerialPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RefreshPortsButton);
            this.Controls.Add(this.PortStatusStrip);
            this.Controls.Add(this.PortsComboBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ExitButton);
            this.Name = "SerialPortForm";
            this.Text = "Form1";
            this.PortStatusStrip.ResumeLayout(false);
            this.PortStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ComboBox PortsComboBox;
        private System.Windows.Forms.StatusStrip PortStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel PortStatusLabel;
        private System.Windows.Forms.Timer PortStatusTimer;
        private System.Windows.Forms.Button RefreshPortsButton;
    }
}

