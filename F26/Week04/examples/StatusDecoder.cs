using System;

public readonly record struct StatusWord(bool Fault, bool Enabled, byte Mode, byte Level);

public static class StatusDecoder
{
    public static StatusWord Decode(byte value)
    {
        return new StatusWord(
            Fault: (value & 0x80) != 0,
            Enabled: (value & 0x40) != 0,
            Mode: (byte)((value >> 3) & 0x07),
            Level: (byte)(value & 0x07));
    }
}

public static class Program
{
    public static void Main()
    {
        byte[] testValues = new byte[] { 0x00, 0xFF, 0x48, 0xB5 };

        foreach (byte value in testValues)
        {
            StatusWord status = StatusDecoder.Decode(value);
            Console.WriteLine(
                $"0x{value:X2} fault={status.Fault} enabled={status.Enabled} " +
                $"mode={status.Mode} level={status.Level}");
        }
    }
}
