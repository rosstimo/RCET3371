using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics.Tracing;
namespace SerialPortExample
{
    public partial class SerialPortForm : Form
    {
        public SerialPortForm()
        {
            InitializeComponent();
            //GetPorts();
            GetQyAtBoards();
        }

        void GetPorts()
        {
            string[] names = SerialPort.GetPortNames();
            foreach (string name in names)
            {
                PortsComboBox.Items.Add(name);
            }
            if (PortsComboBox.Items.Count > 0)
            {
                PortsComboBox.SelectedIndex = 0;
            }
        }
        void GetQyAtBoards()
        {
            string[] names = SerialPort.GetPortNames();
            PortsComboBox.Items.Clear();
            foreach (string name in names)
            {
                //connect
                SerialConnect(name);
                //test if it is QyAt board
                if (IsQyAtBoard())
                {
                    PortsComboBox.Items.Add(name);
                }
                //close port
                serialPort1.Close();
            }
            if (PortsComboBox.Items.Count > 0)
            {
                PortsComboBox.SelectedIndex = 0;
            }
            else
            {   

                PortsComboBox.Items.Add("None");
                PortsComboBox.SelectedIndex = 0;
            }
        }

        void SerialConnect(string portName)
        {
            serialPort1.Close();
            serialPort1.PortName = portName;
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
                Console.WriteLine(ex.Message);
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

        int ReadAnalogOne()
        {
            byte[] data = { 0x51 };
            byte[] response = SendData(data);
            if (response.Length == 2)
            {
                return (response[0] << 2) + (response[1] >> 6);
            }

            return -1; //bad read of analog data
        }
        int ReadAnalogTwo()
        {
            byte[] data = { 0x52 };
            byte[] response = SendData(data);
            if (response.Length == 2)
            {
                return (response[0] << 2) + (response[1] >> 6);
            }

            return -1; //bad read of analog data
        }

        byte[] ReadAnalog(byte analogInputs)
        {
            byte command = 0x50;
            byte[] data = {command};
            byte[] response;
            int highByte, lowByte, value;

            analogInputs = (byte)(0x0f & analogInputs); //zero upper nibble
            command = (byte)(command | analogInputs); //combine nibbles
            //Console.WriteLine($"{command:X2}");


            data[0] = command; //make byte array
            //while (true)
            //{
                response = SendData(data);
                //foreach (byte b in response)
                //{
                //    Console.Write($"{b} ");
                //}
                //Console.WriteLine();
                highByte = response[0] << 2; //make upper byte 10 bit (11111111 to 1111111100)
                lowByte = response[1] >> 6; //shift lsb bits to correct place (11000000 to 00000011)
                value = highByte + lowByte;
                Console.WriteLine(value);

            //}

            return response;
        }

        void UpdatePortStatus()
        {
            if (serialPort1.IsOpen)
            {
                PortStatusLabel.Text = serialPort1.PortName;
            }
            else
            {
                PortStatusLabel.Text = "None";
                //GetQyAtBoards();
            }
        }

        //event handlers below here -------------------------------------------
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialConnect(PortsComboBox.SelectedItem.ToString());

                            // WriteDigital(0x0f);
            //ReadAnalog(0x02);
            //while (true)
            //{
                //Console.WriteLine($"{ReadAnalogOne().ToString().PadLeft(5)} {ReadAnalogTwo().ToString().PadLeft(5)}");
            //}
        }

        private void PortStatusTimer_Tick(object sender, EventArgs e)
        {
            UpdatePortStatus();
        }

        private void RefreshPortsButton_Click(object sender, EventArgs e)
        {
            GetQyAtBoards();
        }
    }
}
