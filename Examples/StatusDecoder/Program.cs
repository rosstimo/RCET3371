static int GetMode(byte status) => (status >> 5) & 0x07;
static bool GetFault(byte status) => (status & 0x10) != 0;
static int GetSequence(byte status) => status & 0x0F;

static byte SetSequence(byte status, int sequence)
{
    if (sequence is < 0 or > 15)
        throw new ArgumentOutOfRangeException(nameof(sequence));
    return (byte)((status & 0xF0) | sequence);
}

byte status = 0b0110_1100;
Console.WriteLine($"mode={GetMode(status)} fault={(GetFault(status) ? 1 : 0)} sequence={GetSequence(status)}");
Console.WriteLine($"updated=0x{SetSequence(status, 2):X2}");
