using System.IO.Ports;

namespace SerialExample
{
    public partial class SerialForm : Form
    {
        public SerialForm()
        {
            InitializeComponent();
            UpdatePortSelection();
        }

        SerialPort _serialPort = new SerialPort();
        void SerialPortSetup(string portName)
        {
            _serialPort.Close();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
        }

        void SerialConnect()
        {
            _serialPort.Close();
            _serialPort.Open();
        }

        void SerialSend(byte[] data)
        {
            try
            {   
                //flush input buffer before send
                _serialPort.ReadExisting();
                //wait a bit
                System.Threading.Thread.Sleep(20);
                //send data
                _serialPort.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            
            }
        }

        byte[] SerialRead()
        {
            byte[] input = new byte[_serialPort.BytesToRead];
            _serialPort.Read(input, 0, input.Length);
            return input;
        }

        string[] GetSerialPorts()
        {
            return SerialPort.GetPortNames();
        }

        void UpdatePortSelection()
        {
            foreach (string port in GetSerialPorts())
            {
                if (IsQyAtBoard(port))
                {
                    PortsComboBox.Items.Add(port);
                }
            }

            if (PortsComboBox.Items.Count > 0)
            {
                PortsComboBox.SelectedIndex = 0;
            }

        }

        void TestQyAtBoard()
        {
            byte[] thingy = { 0xf0 };
            _serialPort.Write(thingy, 0, 1);
        }

        bool IsQyAtBoard(string portName)
        {
            byte[] thingy = { 0xf0 };
            byte[] input = new byte[1];

            SerialPortSetup(portName);
            SerialConnect();

            //flush rx buffer
            input = new byte[_serialPort.BytesToRead];
            _serialPort.Read(input, 0, input.Length);

            //request settings
            _serialPort.Write(thingy, 0, 1);

            //wait for reply
            System.Threading.Thread.Sleep(100);

            //read rx buffer
            input = new byte[_serialPort.BytesToRead];
            _serialPort.Read(input, 0, input.Length);

            //disconnect
            _serialPort.Close();
            //test if QY@ board
            if (input.Length == 64 && input[58] == 81 && input[59] == 121 && input[60] == 64)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        byte[] DigitalWriteQyAt(byte pins = 0x00)
        {
            byte[] command = { 0x20, pins };
            //SerialSend(command);
            return command;
        }

        byte[] AN1Read()
        {
            byte[] command = { 0x51 };
            return command;
        }

        // Event Handlers Below here ******************************************
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialPortSetup(PortsComboBox.SelectedItem.ToString());
            SerialConnect();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            // DigitalWriteQyAt(0xAA);
            //SerialSend(DigitalWriteQyAt(0x55));
            SerialSend(AN1Read());
            //TestQyAtBoard();
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            SerialRead();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            string portName;
            int rxBuffer, txBuffer;
            if (_serialPort.IsOpen)
            {
                portName = _serialPort.PortName;
                rxBuffer = _serialPort.BytesToRead;
                txBuffer = _serialPort.BytesToWrite;
            }
            else
            {
                portName = "none";
                rxBuffer = 0;
                txBuffer = 0;
            }

            StatusLabel.Text = $"Port: {portName} tx:{txBuffer} rx:{rxBuffer}";
        }

        private void AN1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AN1CheckBox.Checked)
            {
                AnalogTimer.Enabled = true;
            }
            else
            {
                AnalogTimer.Enabled = false;
            }
        }

        private void AnalogTimer_Tick(object sender, EventArgs e)
        {
            byte[] data;
            int AN1Base10;
            if (_serialPort.IsOpen)
            {
                try
                {

                    //ComListBox.Items.Add(System.DateTime.Now.ToString("yyMMddhhmmss")+System.DateTime.Now.Millisecond);
                    SerialSend(AN1Read());
                    System.Threading.Thread.Sleep(20);
                    data = SerialRead();
                    AN1Base10 = (data[0] << 2) + (data[1] >> 6);
                
                    ComListBox.Items.Add(AN1Base10);
                    ComListBox.SelectedIndex = ComListBox.Items.Count - 1;
                    ComListBox.ClearSelected();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
