using System.IO.Ports;

namespace SerialExample
{
    public partial class SerialForm : Form
    {
        public SerialForm()
        {
            InitializeComponent();
        }

        SerialPort _serialPort = new SerialPort();
        void SerialPortSetup()
        {
            _serialPort.Close();
            _serialPort.PortName = "COM4";
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
            //_serialPort.StopBits = StopBits.None;


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
            SerialTextBox.Text = _serialPort.ReadExisting();
        }


        // Event Handlers Below here ******************************************
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialPortSetup();
            SerialConnect();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SerialSend();
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
