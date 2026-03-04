using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using QyAt; // your QyBoard lives here

internal static class Program
{
    private const int SettingsLength = 64; // Read Settings returns 64 bytes

    /// <summary>
    /// Simple utility to connect to the Qy@ board over serial, send the Read Settings command, and display the raw and decoded settings.
    /// </summary> <param name="args">Optionally specify the serial port to use as the first argument (e.g., COM3 or /dev/ttyACM0). If not specified, you'll be prompted to select from the list of available ports.</param>
    /// <returns>0 on success, 2 if connection failed, 4 if reading settings failed.</returns>
    public static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        using var sp = Connect(args, out string? portName);
        if (sp == null)
            return 2;

        var settings = ReadSettingsFromBoard(sp);
        if (settings == null)
            return 4;
        Console.WriteLine($"RX ({settings.Length}):");

        HexDump(settings);
        PrintDecodedSettings(settings);

        return 0;
    }

    /// <summary>
    /// Sends the Read Settings command and reads the response from the board.
    /// Returns the settings byte array, or null on error.
    /// </summary>
    private static byte[]? ReadSettingsFromBoard(SerialPort sp)
    {
        // Clear stale bytes before we start.
        sp.DiscardInBuffer();
        sp.DiscardOutBuffer();

        // Send Read Settings command (0xF0) which returns 64 bytes
        byte[] cmd = QyBoard.ReadSettings();
        Console.WriteLine($"TX ({cmd.Length}): {Hex(cmd)}");
        sp.Write(cmd, 0, cmd.Length);

        Thread.Sleep(100);

        try
        {
            return ReadExact(sp, SettingsLength, overallTimeoutMs: 2000);
        }
        catch (TimeoutException)
        {
            Console.WriteLine($"Timed out waiting for {SettingsLength} bytes.");
            Console.WriteLine($"Bytes available right now: {sp.BytesToRead}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Read failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Prompts user to select a serial port, attempts to open it, and returns the open SerialPort instance.
    /// Returns null on failure. Outputs the port name via out parameter.
    /// </summary>
    private static SerialPort? Connect(string[] args, out string? portName)
    {
        var ports = SerialPort.GetPortNames()
            .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        Console.WriteLine("Serial ports visible to .NET:");
        if (ports.Length == 0)
        {
            Console.WriteLine("  (none)");
            Console.WriteLine("If you're on Linux, this is usually permissions (/dev/ttyACM*, /dev/ttyUSB*) or the device isn't enumerated.");
            portName = null;
            return null;
        }

        for (int i = 0; i < ports.Length; i++)
            Console.WriteLine($"  [{i}] {ports[i]}");

        portName = PickPort(args, ports);

        // Note: For the Qy@ USB Virtual COM Port, baud/parity/stop bits are ignored 
        // Still set something reasonable anyway.
        var sp = new SerialPort(portName)
        {
            BaudRate = 115200,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            Handshake = Handshake.None,
            ReadTimeout = 1000,
            WriteTimeout = 1000,
            DtrEnable = true,
            RtsEnable = true
        };

        try
        {
            sp.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to open {portName}: {ex.Message}");
            sp.Dispose();
            portName = null;
            return null;
        }

        Console.WriteLine();
        Console.WriteLine($"Opened: {sp.PortName}");
        Console.WriteLine($"Config: {sp.BaudRate} {sp.DataBits}{sp.Parity}{sp.StopBits}  Handshake={sp.Handshake}");
        Console.WriteLine();
        return sp;
    }

    private static string PickPort(string[] args, string[] ports)
    {
        // CLI usage:
        //   dotnet run -- /dev/ttyACM0
        // or
        //   dotnet run -- 0
        if (args.Length >= 1)
        {
            var a = args[0].Trim();

            if (int.TryParse(a, out int idx) && idx >= 0 && idx < ports.Length)
                return ports[idx];

            // If they passed an actual port path/name, use it.
            if (ports.Contains(a, StringComparer.OrdinalIgnoreCase) || a.StartsWith("/dev/", StringComparison.Ordinal))
                return a;
        }

        while (true)
        {
            Console.Write("Select port index: ");
            var input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out int idx) && idx >= 0 && idx < ports.Length)
                return ports[idx];

            Console.WriteLine("Nope. Enter a valid index.");
        }
    }

    private static byte[] ReadExact(SerialPort sp, int count, int overallTimeoutMs)
    {
        var buffer = new byte[count];
        int offset = 0;

        var start = Environment.TickCount64;
        while (offset < count)
        {
            if (Environment.TickCount64 - start > overallTimeoutMs)
                throw new TimeoutException();

            int available = sp.BytesToRead;
            if (available <= 0)
            {
                Thread.Sleep(5);
                continue;
            }

            int toRead = Math.Min(available, count - offset);
            int read = sp.Read(buffer, offset, toRead);
            if (read > 0) offset += read;
        }

        return buffer;
    }

    private static string Hex(byte[] data)
        => string.Join(" ", data.Select(b => b.ToString("X2")));

    private static void HexDump(byte[] data, int bytesPerLine = 16)
    {
        for (int i = 0; i < data.Length; i += bytesPerLine)
        {
            int len = Math.Min(bytesPerLine, data.Length - i);
            var chunk = new byte[len];
            Array.Copy(data, i, chunk, 0, len);

            string hex = string.Join(" ", chunk.Select(b => b.ToString("X2")));
            Console.WriteLine($"  {i:X2}: {hex}");
        }
    }

    private static void PrintDecodedSettings(byte[] s)
    {
        // Settings bitmap table highlights (addresses)
        // 3 outStreamByte
        // 4 outStreamTimerH
        // 5 outStreamTimerL
        // 8 inStreamByte
        // 9 inStreamTimerH
        // 10 inStreamTimerL
        // 16 analogOutByte (PR4)
        // 17-24 analogOut1..8
        // 26 digitalOutByte0, 27 digitalOutByte1, 28 digitalOutByte2
        // 31-32 USART1 baud H/L
        // 35-38 USART2 baud H/L + pin selects
        // 41 I2Cset, 42 I2Cbaud
        // 45-46 SPIBuadH/SPIBuadL
        // 48 WatchDogSave
        // 58-60 Signature Q y @
        // 61-63 Version bytes

        if (s.Length < 64)
        {
            Console.WriteLine("  Not enough data to decode (need 64 bytes).");
            return;
        }
        Console.WriteLine();
        Console.WriteLine("Decoded settings (partial, useful stuff):");
        string sig = $"{(char)s[58]}{(char)s[59]}{(char)s[60]}"; // Q y @ 
        string ver = $"{(char)s[61]}{(char)s[62]}{(char)s[63]}"; // e.g., 2.0 

        ushort outTimer = (ushort)((s[4] << 8) | s[5]);
        ushort inTimer  = (ushort)((s[9] << 8) | s[10]);

        ushort usart1 = (ushort)((s[31] << 8) | s[32]);
        ushort usart2 = (ushort)((s[35] << 8) | s[36]);

        Console.WriteLine($"  Signature: {sig}");
        Console.WriteLine($"  Version:   {ver}");
        Console.WriteLine();

        Console.WriteLine($"  Out stream byte (addr 03): 0x{s[3]:X2}  (binary {Convert.ToString(s[3], 2).PadLeft(8,'0')})");
        Console.WriteLine($"  Out stream timer (04-05):  0x{outTimer:X4} ({outTimer})");
        Console.WriteLine();

        Console.WriteLine($"  In stream byte (addr 08):  0x{s[8]:X2}  (binary {Convert.ToString(s[8], 2).PadLeft(8,'0')})");
        Console.WriteLine($"  In stream timer (09-0A):   0x{inTimer:X4} ({inTimer})");
        Console.WriteLine();

        Console.WriteLine($"  Analog PR4 (addr 16):      0x{s[16]:X2}");
        Console.WriteLine($"  Analog outputs (17-24):    {string.Join(" ", s.Skip(17).Take(8).Select(b => b.ToString("X2")))}");
        Console.WriteLine();

        Console.WriteLine($"  Digital out boot (26):     0x{s[26]:X2}");
        Console.WriteLine($"  Digital PWM opts (27):     0x{s[27]:X2}");
        Console.WriteLine($"  Digital Servo opts (28):   0x{s[28]:X2}");
        Console.WriteLine();

        Console.WriteLine($"  USART1 baud reg (31-32):   0x{usart1:X4}");
        Console.WriteLine($"  USART2 baud reg (35-36):   0x{usart2:X4}");
        Console.WriteLine($"  USART2 RC pin (37):        0x{s[37]:X2}");
        Console.WriteLine($"  USART2 TX pin (38):        0x{s[38]:X2}");
        Console.WriteLine();

        Console.WriteLine($"  I2C set (41):              0x{s[41]:X2}");
        Console.WriteLine($"  I2C baud/addr (42):        0x{s[42]:X2}");
        Console.WriteLine();

        Console.WriteLine($"  SPI baud H (45):           0x{s[45]:X2}");
        Console.WriteLine($"  SPI baud L (46):           0x{s[46]:X2}");
        Console.WriteLine();

        Console.WriteLine($"  Watchdog save (48):        0x{s[48]:X2}  ({(s[48] == 0 ? "disabled" : "enabled")})");
    }
}