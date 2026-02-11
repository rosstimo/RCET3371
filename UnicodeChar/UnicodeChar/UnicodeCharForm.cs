using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicodeChar
{
    public partial class UnicodeCharForm : Form
    {
        public UnicodeCharForm()
        {
            InitializeComponent();
            SendToFile();
        }

        private void SendToFile()
        {
            //utf-8 encoding for omega symbol
            string ohms = "\u03A9";
            //string ohms = "\u2126";

            //write ohms to a file utf-8 text file text.txt
            using (StreamWriter testFile = File.AppendText("..\\..\\test.txt"))
            {
                testFile.WriteLine(ohms);
            }
        }

        // event handlers below
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnicodeCharForm_Load(object sender, EventArgs e)
        {
            //utf-16 encoding for omega symbol
            //string ohms = "\u2126";
            //utf-8 encoding for omega symbol
            string ohms = "\u03A9";

            textBox1.Text = ohms;
        }


    }
}
