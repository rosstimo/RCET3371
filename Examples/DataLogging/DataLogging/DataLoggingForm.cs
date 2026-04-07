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
            LogFileComboBox.SelectedIndexChanged += LogFileComboBox_SelectedIndexChanged;
            SetDefaults();


        }



        void SetDefaults()
        {
            DisplayPictureBox.BackColor = Color.Black;
            LoadLogFiles();
        }
        void LoadLogFiles()
        {
            try
            {
                string[] logFiles = Directory.GetFiles("..\\..\\logs", "*.log");
                foreach (string file in logFiles)
                {
                    LogFileComboBox.Items.Add(Path.GetFileName(file));
                }

            }
            catch (Exception)
            {
                Directory.CreateDirectory("..\\..\\logs");
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
        float xMax = 100, yMax = 100;
        void GraphDataPoint(int dataX, int dataY)
        {
            //calculate scale factors
            float sx = (float)(DisplayPictureBox.Width / xMax);
            float sy = DisplayPictureBox.Height / yMax;
            Graphics g = DisplayPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Black, 1);
            g.ScaleTransform(sx, sy * -1);//set scale so height and width are 100 units
            g.TranslateTransform(0, -yMax);// move origin down to bottom of graph
            g.DrawLine(thePen, dataX, 0, dataX, yMax); //erase previous data segment
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
            if (this.dataBuffer.Count >= xMax)
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
        void GraphLogFile()
        {
            DataAqTimer.Enabled = false;
            try
            {
                string path = $"..\\..\\logs\\{LogFileComboBox.SelectedItem.ToString()}";
                string[] temp;
                int dataX = 0;
                this.xMax = CountOfLinesIn(path);
                using (StreamReader currentFile = new StreamReader(path))
                {
                    while (!currentFile.EndOfStream)
                    {
                        temp = currentFile.ReadLine().Split(',');
                        GraphDataPoint(dataX, int.Parse(temp[1]));
                        dataX++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            xMax = 100;
        }
        static int CountOfLinesIn(string path)
        {
            int count = 0;
            using (StreamReader currentFile = new StreamReader(path))
            {
                while (!currentFile.EndOfStream)
                {
                    currentFile.ReadLine();
                    count++;
                }
            }
            return count;
        }
        static void LogDataToFile(int currentData)
        {
            try
            {
                string path = $"..\\..\\logs\\{DateTime.Now.ToString("yyyyMMddhh")}_data.log";
                using (StreamWriter currentFile = File.AppendText(path))
                {
                    currentFile.WriteLine($"{DateTime.Now:yyyyMMddhhmmss}{DateTime.Now.Millisecond:D3},{currentData}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void LogFileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GraphLogFile();
        }
    }
}
