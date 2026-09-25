using System.Collections.Generic;

var parser = new Parser();
foreach (byte[] chunk in new[]
{
    new byte[] { 0x00, 0xA5 },
    new byte[] { 0x01, 0x00 },
    new byte[] { 0x01, 0xA5, 0x10, 0x00, 0x10 }
})
{
    foreach (Frame frame in parser.Feed(chunk))
        Console.WriteLine($"type=0x{frame.Type:X2} len={frame.Payload.Length}");
}

readonly record struct Frame(byte Type, byte[] Payload);

sealed class Parser
{
    private readonly List<byte> _buffer = new();

    public IEnumerable<Frame> Feed(IEnumerable<byte> bytes)
    {
        _buffer.AddRange(bytes);

        while (true)
        {
            int start = _buffer.IndexOf(0xA5);
            if (start < 0)
            {
                _buffer.Clear();
                yield break;
            }

            if (start > 0)
                _buffer.RemoveRange(0, start);

            if (_buffer.Count < 4)
                yield break;

            int length = _buffer[2];
            if (length > 16)
            {
                _buffer.RemoveAt(0);
                continue;
            }

            int frameLength = 4 + length;
            if (_buffer.Count < frameLength)
                yield break;

            byte check = (byte)(_buffer[1] ^ _buffer[2]);
            for (int i = 0; i < length; i++)
                check ^= _buffer[3 + i];

            if (check != _buffer[3 + length])
            {
                _buffer.RemoveAt(0);
                continue;
            }

            byte type = _buffer[1];
            byte[] payload = _buffer.GetRange(3, length).ToArray();
            _buffer.RemoveRange(0, frameLength);
            yield return new Frame(type, payload);
        }
    }
}
