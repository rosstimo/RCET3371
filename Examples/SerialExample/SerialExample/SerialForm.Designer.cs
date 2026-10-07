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
            components = new System.ComponentModel.Container();
            ExitButton = new Button();
            ConnectButton = new Button();
            ReadButton = new Button();
            SendButton = new Button();
            SerialTextBox = new TextBox();
            statusStrip1 = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            StatusTimer = new System.Windows.Forms.Timer(components);
            PortsComboBox = new ComboBox();
            ComListBox = new ListBox();
            AnalogTimer = new System.Windows.Forms.Timer(components);
            AN1CheckBox = new CheckBox();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(434, 223);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(92, 47);
            ExitButton.TabIndex = 0;
            ExitButton.Text = "E&xit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(336, 223);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(92, 47);
            ConnectButton.TabIndex = 2;
            ConnectButton.Text = "&Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // ReadButton
            // 
            ReadButton.Location = new Point(238, 223);
            ReadButton.Name = "ReadButton";
            ReadButton.Size = new Size(92, 47);
            ReadButton.TabIndex = 3;
            ReadButton.Text = "&Read";
            ReadButton.UseVisualStyleBackColor = true;
            ReadButton.Click += ReadButton_Click;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(140, 223);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(92, 47);
            SendButton.TabIndex = 4;
            SendButton.Text = "&Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // SerialTextBox
            // 
            SerialTextBox.Location = new Point(12, 50);
            SerialTextBox.Name = "SerialTextBox";
            SerialTextBox.Size = new Size(121, 23);
            SerialTextBox.TabIndex = 5;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel });
            statusStrip1.Location = new Point(0, 273);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(538, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(34, 17);
            StatusLabel.Text = "none";
            // 
            // StatusTimer
            // 
            StatusTimer.Enabled = true;
            StatusTimer.Interval = 250;
            StatusTimer.Tick += StatusTimer_Tick;
            // 
            // PortsComboBox
            // 
            PortsComboBox.FormattingEnabled = true;
            PortsComboBox.Location = new Point(12, 12);
            PortsComboBox.Name = "PortsComboBox";
            PortsComboBox.Size = new Size(121, 23);
            PortsComboBox.TabIndex = 7;
            // 
            // ComListBox
            // 
            ComListBox.FormattingEnabled = true;
            ComListBox.Location = new Point(140, 12);
            ComListBox.Name = "ComListBox";
            ComListBox.Size = new Size(386, 199);
            ComListBox.TabIndex = 8;
            // 
            // AnalogTimer
            // 
            AnalogTimer.Interval = 250;
            AnalogTimer.Tick += AnalogTimer_Tick;
            // 
            // AN1CheckBox
            // 
            AN1CheckBox.AutoSize = true;
            AN1CheckBox.Location = new Point(12, 79);
            AN1CheckBox.Name = "AN1CheckBox";
            AN1CheckBox.Size = new Size(49, 19);
            AN1CheckBox.TabIndex = 9;
            AN1CheckBox.Text = "AN1";
            AN1CheckBox.UseVisualStyleBackColor = true;
            AN1CheckBox.CheckedChanged += AN1CheckBox_CheckedChanged;
            // 
            // SerialForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(538, 295);
            Controls.Add(AN1CheckBox);
            Controls.Add(ComListBox);
            Controls.Add(PortsComboBox);
            Controls.Add(statusStrip1);
            Controls.Add(SerialTextBox);
            Controls.Add(SendButton);
            Controls.Add(ReadButton);
            Controls.Add(ConnectButton);
            Controls.Add(ExitButton);
            Name = "SerialForm";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ExitButton;
        private Button ConnectButton;
        private Button ReadButton;
        private Button SendButton;
        private TextBox SerialTextBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Timer StatusTimer;
        private ComboBox PortsComboBox;
        private ListBox ComListBox;
        private System.Windows.Forms.Timer AnalogTimer;
        private CheckBox AN1CheckBox;
    }
}
