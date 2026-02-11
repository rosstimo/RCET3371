using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyPress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateKeyDisplay(e, "DOWN");
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            lblKeyDisplay.Text = "Press any key...";
        }

        private void UpdateKeyDisplay(KeyEventArgs e, string state)
        {
            StringBuilder keyInfo = new StringBuilder();
            
            keyInfo.AppendLine($"Key: {e.KeyCode}");
            keyInfo.AppendLine($"KeyData: {e.KeyData}");
            keyInfo.AppendLine($"KeyValue: {e.KeyValue}");
            keyInfo.AppendLine();
            
            keyInfo.AppendLine("Modifiers:");
            keyInfo.AppendLine($"  Ctrl: {e.Control}");
            keyInfo.AppendLine($"  Alt: {e.Alt}");
            keyInfo.AppendLine($"  Shift: {e.Shift}");
            
            lblKeyDisplay.Text = keyInfo.ToString();
        }
    }
}
