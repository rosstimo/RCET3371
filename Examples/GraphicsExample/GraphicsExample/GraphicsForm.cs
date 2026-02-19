using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsExample
{
    public partial class GraphicsForm : Form
    {
        public GraphicsForm()
        {
            InitializeComponent();
        }

        void DrawLine()
        {
            Graphics g = DisplayPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Red, 1);
            
            g.DrawLine(thePen, 0, 0, 100, 100);

            g.Dispose();
            thePen.Dispose();
        }

        void DrawDart(int x, int y)
        {
            Graphics g = DisplayPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Blue, 1);
            int size = 30;

            g.DrawEllipse(thePen, x - size/2, y - size/2, size, size);

            g.DrawLine(thePen, x - 5, y, x + 5, y);
            g.DrawLine(thePen, x, y - 5, x, y + 5);

            g.Dispose();
            thePen.Dispose();
        }


        // Event handlers below _______________________________________________
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            DrawLine();
            DrawDart(100,100);
        }
    }
}
