using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SerialPortExample
{
    public partial class SerialPortForm : Form
    {
        public SerialPortForm()
        {
            InitializeComponent();
        }

        void SerialConnect()
        {
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = System.IO.Ports.Parity.None;
            //serialPort1.StopBits = System.IO.Ports.StopBits.None;
            try
            {
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show($"{serialPort1.PortName} connected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
          
            }
            //Console.WriteLine("hello");
            byte[] QyAtGetSettings = {0xf0};
            byte[] ReadBuffer = SendData(QyAtGetSettings);
        }

        byte[] SendData(byte[] data)
        {
            //flush old bytes from buffers
            serialPort1.DiscardInBuffer();
            serialPort1.DiscardOutBuffer();

            //send command
            serialPort1.Write(data, 0, data.Length);

            //wait for response
            Thread.Sleep(100);

            byte[] buffer = new byte[serialPort1.BytesToRead];
            
            serialPort1.Read(buffer,0, buffer.Length);
            return buffer;
        }



        //event handlers below here -------------------------------------------
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialConnect();
        }
    }
}
