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
                    //MessageBox.Show($"{serialPort1.PortName} connected");
                    //TODO connection status label
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
          
            }
            //Console.WriteLine("hello");
        }

        bool IsQyAtBoard()
        {   
            bool _isQyAtBoard = false;
            byte[] QyAtGetSettings = {0xf0};
            byte[] ReadBuffer = SendData(QyAtGetSettings);
            int byteNumber = 1;

            if (ReadBuffer.Length == 64 && ReadBuffer[58] == 81 && ReadBuffer[59] == 121 && ReadBuffer[60] == 64)
            {
                //MessageBox.Show($"Connected to a Qy@ board on {serialPort1.PortName}");
                _isQyAtBoard = true;
            }

            //print settings to console
            foreach (byte b in ReadBuffer)
            {
                Console.WriteLine($"{byteNumber}: {b:X2} {b} {(char)b}");
                byteNumber++;
            }
            return _isQyAtBoard;
        }

        byte[] SendData(byte[] data)
        {
            byte[] buffer = new byte[0];
            if (serialPort1.IsOpen)
            {
                //flush old bytes from buffers
                serialPort1.DiscardInBuffer();
                serialPort1.DiscardOutBuffer();

                //send command
                serialPort1.Write(data, 0, data.Length);

                //wait for response
                Thread.Sleep(100);
                
                //make the array the size of the input buffer
                buffer = new byte[serialPort1.BytesToRead];

                //actually read the input buffer
                serialPort1.Read(buffer, 0, buffer.Length);
            }
                return buffer;
        }

        void WriteDigital(byte pins = 0)
        {
            byte[] data = {0x20, pins};
            SendData(data);
        }




        //event handlers below here -------------------------------------------
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialConnect();
            WriteDigital(0x0f);
        }
    }
}
