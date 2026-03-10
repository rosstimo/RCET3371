using System;
using System.IO.Ports;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Serial Port Test Utility\n");
        var ports = SerialPort.GetPortNames();
        if (ports.Length == 0)
        {
            Console.WriteLine("No serial ports found.");
            return;
        }
        Console.WriteLine("Available serial ports:");
        foreach (var port in ports)
        {
            Console.WriteLine($"  {port}");
        }

        Console.Write("Enter port name to open (e.g., COM3 or /dev/ttyUSB0): ");
        var portName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(portName))
        {
            Console.WriteLine("No port selected. Exiting.");
            return;
        }

        try
        {
            using var serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();
            Console.WriteLine($"Opened {portName}. Type text to send, or 'exit' to quit.");
            serialPort.DataReceived += (s, e) =>
            {
                try
                {
                    var sp = (SerialPort)s!;
                    string data = sp.ReadExisting();
                    Console.Write($"[RECV] {data}");
                }
                catch { }
            };
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null || input.Trim().ToLower() == "exit")
                    break;
                serialPort.WriteLine(input);
            }
            serialPort.Close();
            Console.WriteLine("Port closed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
