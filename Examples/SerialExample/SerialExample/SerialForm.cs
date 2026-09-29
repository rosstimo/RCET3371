using System.IO.Ports;

namespace SerialExample
{
    public partial class SerialForm : Form
    {
        public SerialForm()
        {
            InitializeComponent();
        }

        private SerialPort _serialPort;
        void SerialPortSetup()
        {
            _serialPort.PortName = "COM4";
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.None;
            
        }


        // Event Handlers Below here ******************************************
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SerialPortSetup();
        }
    }
}
