using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QBoardTesting
{
    public partial class QBoardForm : Form
    {
        public QBoardForm()
        {
            InitializeComponent();
            ExitButton.Click += ExitButton_Click;
            SendButton.Click += SendButton_Click;
        }

        private void ConfigurePort()
        {

        }

        private void Connect()
        {
            try
            {
                SerialPort.PortName = "COM3";
                SerialPort.BaudRate = 115200;

                SerialPort.Open();
                MessageBox.Show("Connected to QBoard successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to QBoard: {ex.Message}");
            }


        }

        private void Disconnect()
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
                MessageBox.Show("Disconnected from QBoard.");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Disconnect();
            Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void SendAndRead()//TODO take byte[] as arg then return byte[] received
        {
            byte[] buffer = { 0xF0 };
            byte[] readBuffer;

            if (!SerialPort.IsOpen)
            {
                MessageBox.Show("Serial port is not open.");
            }
            else
            {
                SerialPort.DiscardInBuffer();
                SerialPort.Write(buffer, 0, buffer.Length);
                System.Threading.Thread.Sleep(100); // Wait for response
                readBuffer = new byte[SerialPort.BytesToRead];
                SerialPort.Read(readBuffer, 0, readBuffer.Length);
                //SerialPort.Read(readBuffer, 61, 1);

                Console.WriteLine($"There are {readBuffer.Length} bytes");
                Console.WriteLine((char)readBuffer[60]);
                foreach(byte b in readBuffer)
                {
                    //convert byte to hex and show ASCII char and write to console in columns
                    Console.WriteLine($"{b:X2} {b} {(char)b}");
                }
            }

            
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendAndRead();
        }
    }
}
