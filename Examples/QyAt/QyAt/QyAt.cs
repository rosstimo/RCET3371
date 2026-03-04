using System;
using System.Linq;

namespace QyAt
{
    [Flags]
    public enum AnalogInputMask : byte
    {
        None = 0,
        A1   = 1 << 0,
        A2   = 1 << 1,
        A3   = 1 << 2,
        A4   = 1 << 3,
        All  = A1 | A2 | A3 | A4
    }

    public static class QyBoard
    {
        // Command numbers (these become the HIGH nibble).
        // Datasheet: command is bits 7-4, argument is bits 3-0.
        private const byte CMD_NULL                 = 0x0;
        private const byte CMD_READ_STATUS          = 0x1;
        private const byte CMD_WRITE_DIGITAL_OUT    = 0x2;
        private const byte CMD_READ_DIGITAL_IN      = 0x3;
        private const byte CMD_WRITE_ANALOG_OUT     = 0x4;
        private const byte CMD_READ_ANALOG_IN       = 0x5;
        private const byte CMD_WRITE_USART1         = 0x6;
        private const byte CMD_READ_USART1_BUFFER   = 0x7;
        private const byte CMD_WRITE_USART2         = 0x8;
        private const byte CMD_READ_USART2_BUFFER   = 0x9;
        private const byte CMD_WRITE_SPI            = 0xA;
        private const byte CMD_READ_SPI_BUFFER      = 0xB;
        private const byte CMD_I2C_TRANSACTION       = 0xC;
        private const byte CMD_SD_TRANSACTION        = 0xD; // reserved / future
        private const byte CMD_WRITE_SETTINGS        = 0xE;
        private const byte CMD_END_PACKET_OR_SETTINGS= 0xF;

        // Settings numbers (argument nibble for CMD_WRITE_SETTINGS).
        public static class Setting
        {
            public const byte Sleep                 = 0;
            public const byte DeviceToHostStreaming = 1;
            public const byte HostToDeviceStreaming = 2; // datasheet section is marked "Incomplete"
            public const byte AnalogInputOptions    = 3; // reserved/unimplemented
            public const byte AnalogOutputOptions   = 4;
            public const byte DigitalOutputOptions  = 5;
            public const byte Usart1BaudRate        = 6;
            public const byte Usart2Config          = 7;
            public const byte SpiBaudRate           = 8;
            public const byte I2cBaudRateOrSetup    = 9;
            public const byte SdCardSettings        = 10; // reserved
            public const byte Reserved11            = 11;
            public const byte StoreCurrentOutputs   = 12;
            public const byte WatchDogTimer         = 13; // described later in datasheet
            public const byte SaveSettings          = 14;
            public const byte Reset                 = 15;
        }

        // ---------- Core helpers ----------

        public static byte BuildCommandByte(byte commandNumber, byte argument = 0)
        {
            return (byte)(((commandNumber & 0x0F) << 4) | (argument & 0x0F));
        }

        private static void RequireNibble(byte value, string name)
        {
            if (value > 0x0F) throw new ArgumentOutOfRangeException(name, "Must fit in a 4-bit nibble (0-15).");
        }

        private static byte EncodeLengthNibble_16Max(int length)
        {
            // Datasheet says some things are 1-16 bytes, but we only have a 4-bit nibble.
            // Common convention: 0 means 16. If your firmware does something else, change it here.
            if (length < 1 || length > 16) throw new ArgumentOutOfRangeException(nameof(length), "Length must be 1-16.");
            return (byte)(length == 16 ? 0 : length);
        }

        public static byte[] Concat(params byte[][] parts)
        {
            if (parts == null) throw new ArgumentNullException(nameof(parts));
            int total = parts.Sum(p => p?.Length ?? 0);
            var result = new byte[total];
            int offset = 0;

            foreach (var p in parts)
            {
                if (p == null) continue;
                Buffer.BlockCopy(p, 0, result, offset, p.Length);
                offset += p.Length;
            }

            return result;
        }

        // ---------- Command 0-15 builders ----------

        // 0: Null (end of packet marker for some links)
        public static byte[] Null() => new[] { BuildCommandByte(CMD_NULL) };

        // 1: Read Status
        public static byte[] ReadStatus() => new[] { BuildCommandByte(CMD_READ_STATUS) };

        // 2: Write Digital Outputs (1 data byte)
        public static byte[] WriteDigitalOutputs(byte outputs)
            => new[] { BuildCommandByte(CMD_WRITE_DIGITAL_OUT), outputs };

        // 3: Read Digital Inputs
        public static byte[] ReadDigitalInputs() => new[] { BuildCommandByte(CMD_READ_DIGITAL_IN) };

        // 4: Write to Analog Output / PWM / Servo (2 data bytes, argument selects channel)
        // Datasheet: first byte upper 8 bits, second byte carries lower 2 bits.
        public static byte[] WriteAnalogOutput(byte channel, ushort value10Bit)
        {
            RequireNibble(channel, nameof(channel));
            if (value10Bit > 1023) throw new ArgumentOutOfRangeException(nameof(value10Bit), "10-bit value must be 0-1023.");

            byte high = (byte)(value10Bit >> 2);           // bits 9..2
            byte low  = (byte)((value10Bit & 0x03) << 6);  // bits 1..0 in bits 7..6

            return new[] { BuildCommandByte(CMD_WRITE_ANALOG_OUT, channel), high, low };
        }

        // Optional convenience if you already have the split bytes.
        public static byte[] WriteAnalogOutputRaw(byte channel, byte highByte, byte lowByte)
        {
            RequireNibble(channel, nameof(channel));
            return new[] { BuildCommandByte(CMD_WRITE_ANALOG_OUT, channel), highByte, lowByte };
        }

        // 5: Read Analog Inputs (argument is a 4-bit mask selecting A1..A4)
        public static byte[] ReadAnalogInputs(AnalogInputMask mask)
        {
            byte arg = (byte)((byte)mask & 0x0F);
            if (arg == 0) throw new ArgumentException("Mask must select at least one analog input (A1-A4).", nameof(mask));
            return new[] { BuildCommandByte(CMD_READ_ANALOG_IN, arg) };
        }

        // 6: Write to USART1 (argument = number of bytes that follow, 1-15)
        public static byte[] WriteUsart1(params byte[] data)
            => WriteVariableLength(CMD_WRITE_USART1, 15, data);

        // 7: Read USART1 buffer (argument not used per description)
        public static byte[] ReadUsart1Buffer() => new[] { BuildCommandByte(CMD_READ_USART1_BUFFER) };

        // 8: Write to USART2 (argument = number of bytes that follow, 1-15)
        public static byte[] WriteUsart2(params byte[] data)
            => WriteVariableLength(CMD_WRITE_USART2, 15, data);

        // 9: Read USART2 buffer
        public static byte[] ReadUsart2Buffer() => new[] { BuildCommandByte(CMD_READ_USART2_BUFFER) };

        // 10: Write to SPI (argument = number of bytes that follow, 1-15)
        public static byte[] WriteSpi(params byte[] data)
            => WriteVariableLength(CMD_WRITE_SPI, 15, data);

        // 11: Read SPI buffer
        public static byte[] ReadSpiBuffer() => new[] { BuildCommandByte(CMD_READ_SPI_BUFFER) };

        // 12: I2C Transaction
        // Packet is: [cmdByte][addressByte][optional write data...]
        // For reads: you still specify N (bytes to read) but provide no data after address.
        public static byte[] I2cWrite(byte address7Bit, params byte[] writeData)
        {
            if (address7Bit > 0x7F) throw new ArgumentOutOfRangeException(nameof(address7Bit), "7-bit address must be 0-127.");
            if (writeData == null) throw new ArgumentNullException(nameof(writeData));
            if (writeData.Length < 1 || writeData.Length > 16) throw new ArgumentOutOfRangeException(nameof(writeData), "Write data must be 1-16 bytes.");

            byte addressByte = (byte)((address7Bit << 1) | 0); // RW=0
            return I2cTransaction(addressByte, writeData.Length, writeData);
        }

        public static byte[] I2cRead(byte address7Bit, int bytesToRead)
        {
            if (address7Bit > 0x7F) throw new ArgumentOutOfRangeException(nameof(address7Bit), "7-bit address must be 0-127.");
            if (bytesToRead < 1 || bytesToRead > 16) throw new ArgumentOutOfRangeException(nameof(bytesToRead), "Read count must be 1-16.");

            byte addressByte = (byte)((address7Bit << 1) | 1); // RW=1
            return I2cTransaction(addressByte, bytesToRead, null);
        }

        // If you want full control: supply addressByte including RW bit (LSB).
        public static byte[] I2cTransaction(byte addressByteWithRw, int count, byte[]? writeData)
        {
            // "count" means: bytes written (if write) OR bytes requested (if read)
            byte arg = EncodeLengthNibble_16Max(count);

            if (writeData != null && writeData.Length != count)
                throw new ArgumentException("writeData length must match count for write transactions.", nameof(writeData));

            int payloadLen = (writeData?.Length ?? 0);
            var packet = new byte[2 + payloadLen];

            packet[0] = BuildCommandByte(CMD_I2C_TRANSACTION, arg);
            packet[1] = addressByteWithRw;

            if (payloadLen > 0)
                Buffer.BlockCopy(writeData!, 0, packet, 2, payloadLen);

            return packet;
        }

        // 13: SD Card Transaction (reserved)
        public static byte[] SdTransaction(params byte[] data)
        {
            // Keeping it generic, since the datasheet flags it as future/reserved.
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length < 1 || data.Length > 16) throw new ArgumentOutOfRangeException(nameof(data), "SD transaction data must be 1-16 bytes.");

            byte arg = EncodeLengthNibble_16Max(data.Length);
            var packet = new byte[1 + data.Length];
            packet[0] = BuildCommandByte(CMD_SD_TRANSACTION, arg);
            Buffer.BlockCopy(data, 0, packet, 1, data.Length);
            return packet;
        }

        // 14: Write to Settings (argument selects which setting; data length varies by setting)
        public static byte[] WriteSetting(byte settingNumber, params byte[] data)
        {
            RequireNibble(settingNumber, nameof(settingNumber));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length > 4) throw new ArgumentOutOfRangeException(nameof(data), "Settings payload must be 0-4 bytes.");

            var packet = new byte[1 + data.Length];
            packet[0] = BuildCommandByte(CMD_WRITE_SETTINGS, settingNumber);

            if (data.Length > 0)
                Buffer.BlockCopy(data, 0, packet, 1, data.Length);

            return packet;
        }

        // 15: End-of-packet / Read settings
        public static byte[] ReadSettings() => new[] { BuildCommandByte(CMD_END_PACKET_OR_SETTINGS, 0x0) };

        public static byte[] EndOfPacket(byte nonZeroArgument = 0xF)
        {
            if (nonZeroArgument == 0) throw new ArgumentOutOfRangeException(nameof(nonZeroArgument), "Argument must be non-zero to mean 'end of packet'.");
            RequireNibble(nonZeroArgument, nameof(nonZeroArgument));
            return new[] { BuildCommandByte(CMD_END_PACKET_OR_SETTINGS, nonZeroArgument) };
        }

        // ---------- Setup menu helpers (these are just WriteSetting wrappers) ----------

        public static byte[] Sleep() => WriteSetting(Setting.Sleep);

        // Device to Host Streaming: 3 bytes (selection, timer high, timer low)
        public static byte[] DeviceToHostStreaming(byte selectionBits, ushort timer)
            => WriteSetting(Setting.DeviceToHostStreaming, selectionBits, (byte)(timer >> 8), (byte)(timer & 0xFF));

        // Host to Device Streaming: datasheet section exists but is labeled incomplete.
        // It shows 2 selection bytes. The table says "3", but the described fields are 2 bytes.
        public static byte[] HostToDeviceStreaming(byte selectionBits1, byte selectionBits2)
            => WriteSetting(Setting.HostToDeviceStreaming, selectionBits1, selectionBits2);

        public static byte[] AnalogOutputOptions(byte periodRegisterValue)
            => WriteSetting(Setting.AnalogOutputOptions, periodRegisterValue);

        public static byte[] DigitalOutputOptions(byte optionsByte1, byte optionsByte2)
            => WriteSetting(Setting.DigitalOutputOptions, optionsByte1, optionsByte2);

        public static byte[] Usart1BaudRate(ushort baudRegisterValue)
            => WriteSetting(Setting.Usart1BaudRate, (byte)(baudRegisterValue >> 8), (byte)(baudRegisterValue & 0xFF));

        public static byte[] Usart2Config(ushort baudRegisterValue, byte rcPinLocation, byte txPinLocation)
            => WriteSetting(Setting.Usart2Config,
                (byte)(baudRegisterValue >> 8),
                (byte)(baudRegisterValue & 0xFF),
                rcPinLocation,
                txPinLocation);

        public static byte[] SpiBaudRate(byte baudHigh, byte baudLow)
            => WriteSetting(Setting.SpiBaudRate, baudHigh, baudLow);

        public static byte[] I2cBaudRateOrDisable(byte baud)
            => WriteSetting(Setting.I2cBaudRateOrSetup, baud);

        public static byte[] StoreCurrentOutputs()
            => WriteSetting(Setting.StoreCurrentOutputs);

        public static byte[] WatchDogTimer(bool enable)
            => WriteSetting(Setting.WatchDogTimer, (byte)(enable ? 0x01 : 0x00));

        public static byte[] SaveSettings()
            => WriteSetting(Setting.SaveSettings);

        public static byte[] Reset()
            => WriteSetting(Setting.Reset);

        // ---------- Small utility: decode the board's 10-bit format ----------
        public static ushort Decode10Bit(byte highByte, byte lowByte)
        {
            // high is bits 9..2, low uses bits 7..6 for bits 1..0
            return (ushort)((highByte << 2) | (lowByte >> 6));
        }

        // ---------- Private helper ----------
        private static byte[] WriteVariableLength(byte cmdNumber, int maxLen, byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length < 1 || data.Length > maxLen)
                throw new ArgumentOutOfRangeException(nameof(data), $"Payload must be 1-{maxLen} bytes.");

            var packet = new byte[1 + data.Length];
            packet[0] = BuildCommandByte(cmdNumber, (byte)data.Length); // argument = N
            Buffer.BlockCopy(data, 0, packet, 1, data.Length);
            return packet;
        }
    }
}