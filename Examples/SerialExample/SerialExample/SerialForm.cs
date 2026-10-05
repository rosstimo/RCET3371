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

        void SerialSend()
        {
            _serialPort.Write("hello");
        }

        void SerialRead()
        {
            byte[] input = new byte[_serialPort.BytesToRead];
            int byteNumber = 0;
            _serialPort.Read(input,0, input.Length);

            foreach (byte b in input)
            {
                byteNumber++;
                ComListBox.Items.Add($"{byteNumber}: {b:X2} : {(char)b} : {b}");
            }

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
            _serialPort.Read(input,  0, input.Length);

            //request settings
            _serialPort.Write(thingy, 0, 1);

            //wait for reply
            System.Threading.Thread.Sleep(100);

            //read rx buffer
            input = new byte[_serialPort.BytesToRead];
            _serialPort.Read(input,0, input.Length);

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
            //SerialSend();
            TestQyAtBoard();
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
    }
}
