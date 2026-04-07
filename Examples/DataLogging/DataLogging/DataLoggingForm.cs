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

namespace DataLogging
{
    public partial class DataLoggingForm : Form
    {
        private List<int> dataBuffer = new List<int>(); // data y values
        public DataLoggingForm()
        {
            InitializeComponent();
            SetDefaults();


        }

        void SetDefaults()
        {
            DisplayPictureBox.BackColor = Color.Black;
            LoadLogFiles();
        }
        void LoadLogFiles()
        {
            string[] logFiles = Directory.GetFiles("..\\..\\logs", "*.log");
            foreach (string file in logFiles)
            {
                LogFileComboBox.Items.Add(Path.GetFileName(file));
            }
        }
        private static int oldX = 0;
        void DrawVerticalLine(int newX)
        {
            Graphics g = DisplayPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Black, 1);
            g.DrawLine(thePen, oldX, 0, oldX, DisplayPictureBox.Height); //erase old line
            thePen.Color = Color.Lime;
            g.DrawLine(thePen, 0, 0, 50, 50);

            g.DrawLine(thePen, newX, 0, newX, DisplayPictureBox.Height); //draw new line
            oldX = newX; //update oldX for next time
            g.Dispose();
            thePen.Dispose();
        }

        int lastY = 0;
        void GraphDataPoint(int dataX, int dataY)
        {
            //calculate scale factors
            float sx = DisplayPictureBox.Width / 100F;
            float sy = DisplayPictureBox.Height / 100F;
            Graphics g = DisplayPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Black, 1);
            g.ScaleTransform(sx, sy * -1);//set scale so height and width are 100 units
            g.TranslateTransform(0, -100);// move origin down to bottom of graph
            g.DrawLine(thePen, dataX, 0, dataX, 100); //erase previous data segment
            thePen.Width = 0.25F; //make the pen width thinner after scaling
            thePen.Color = Color.Lime;
            g.DrawLine(thePen, dataX - 1, lastY, dataX, dataY);// draw new data segment
            lastY = dataY;
            g.Dispose();
            thePen.Dispose();
        }


        void GetDataPoint()
        {
            int currentData = RandomNumberBetween(55,45); //Acquire the data
            if (this.dataBuffer.Count >= 100)
            {
                dataBuffer.RemoveAt(0);
            }
            this.dataBuffer.Add(currentData);
            LogDataToFile(currentData);

        }

        private readonly Random random = new Random(); // Single instance to ensure unique random numbers
        private int RandomNumberBetween(int max, int min = 0)
        {
            int temp = max - min;
            return random.Next(0, temp + 1) + min;
        }

        void UpdateGraph()
        {
            //DisplayPictureBox.Refresh(); this causes visible flicker
            int dataX = 0;
            foreach (int dataY in this.dataBuffer)
            {
                GraphDataPoint(dataX, dataY);
                dataX++;
            }
        }
        static void LogDataToFile(int currentData)
        {
            string path = $"..\\..\\logs\\{DateTime.Now.ToString("yyyyMMddhh")}_data.log";
            using (StreamWriter currentFile = File.AppendText(path))
            {
                currentFile.WriteLine($"{DateTime.Now:yyyyMMddhhmmss}{DateTime.Now.Millisecond:D3},{currentData}");
            }
        }

        //Event Handlers ------------------------------------------------------
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GraphButton_Click(object sender, EventArgs e)
        {
            if (DataAqTimer.Enabled)
            {
                DataAqTimer.Enabled = false;
            }
            else
            {
                DataAqTimer.Enabled = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void DisplayPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = e.X.ToString();
            DrawVerticalLine(e.X);
        }

        private void DataAqTimer_Tick(object sender, EventArgs e)
        {
            GetDataPoint();
            UpdateGraph();
        }
    }
}
