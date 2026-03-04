// File: QyAt.Visualize/Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using QyAt;

namespace QyAt.Visualize
{
    internal static class Program
    {
        private sealed record Case(string Name, byte[] Actual, byte[] Expected);

        private static int Main()
        {
            var cases = BuildCases();
            int fails = 0;

            Console.WriteLine("Qy@ Command Visualizer / Self-Test");
            Console.WriteLine(new string('-', 60));

            foreach (var c in cases)
            {
                bool ok = c.Actual.SequenceEqual(c.Expected);
                if (!ok) fails++;

                PrintCase(c, ok);
            }

            Console.WriteLine(new string('-', 60));
            Console.WriteLine(fails == 0
                ? "ALL TESTS PASSED"
                : $"{fails} TEST(S) FAILED");

            // non-zero exit is handy for CI or scripts
            return fails == 0 ? 0 : 1;
        }

        private static List<Case> BuildCases()
        {
            static byte[] B(params byte[] b) => b;
            static byte[] Seq(int startInclusive, int count)
                => Enumerable.Range(startInclusive, count).Select(i => (byte)i).ToArray();

            var data15 = Seq(1, 15);
            var data16 = Seq(0x20, 16);

            return new List<Case>
            {
                new("Null()", QyBoard.Null(), B(0x00)),
                new("ReadStatus()", QyBoard.ReadStatus(), B(0x10)),
                new("WriteDigitalOutputs(0xA5)", QyBoard.WriteDigitalOutputs(0xA5), B(0x20, 0xA5)),
                new("ReadDigitalInputs()", QyBoard.ReadDigitalInputs(), B(0x30)),

                new("WriteAnalogOutput(ch=2, val=512)", QyBoard.WriteAnalogOutput(2, 512), B(0x42, 0x80, 0x00)),
                new("WriteAnalogOutput(ch=15, val=1023)", QyBoard.WriteAnalogOutput(15, 1023), B(0x4F, 0xFF, 0xC0)),
                new("ReadAnalogInputs(A1|A4)", QyBoard.ReadAnalogInputs(AnalogInputMask.A1 | AnalogInputMask.A4), B(0x59)),

                new("WriteUsart1([01])", QyBoard.WriteUsart1(0x01), B(0x61, 0x01)),
                new("WriteUsart1(len=15)", QyBoard.WriteUsart1(data15), B(0x6F).Concat(data15).ToArray()),
                new("ReadUsart1Buffer()", QyBoard.ReadUsart1Buffer(), B(0x70)),

                new("WriteUsart2([AA BB CC])", QyBoard.WriteUsart2(0xAA, 0xBB, 0xCC), B(0x83, 0xAA, 0xBB, 0xCC)),
                new("ReadUsart2Buffer()", QyBoard.ReadUsart2Buffer(), B(0x90)),

                new("WriteSpi([DE AD])", QyBoard.WriteSpi(0xDE, 0xAD), B(0xA2, 0xDE, 0xAD)),
                new("ReadSpiBuffer()", QyBoard.ReadSpiBuffer(), B(0xB0)),

                new("I2cWrite(addr=0x50, [AA BB CC])", QyBoard.I2cWrite(0x50, 0xAA, 0xBB, 0xCC), B(0xC3, 0xA0, 0xAA, 0xBB, 0xCC)),
                new("I2cRead(addr=0x50, n=4)", QyBoard.I2cRead(0x50, 4), B(0xC4, 0xA1)),
                new("I2cTransaction(count=16 -> arg=0)", QyBoard.I2cTransaction(0xA0, 16, data16), B(0xC0, 0xA0).Concat(data16).ToArray()),

                new("SdTransaction(len=16 -> arg=0)", QyBoard.SdTransaction(data16), B(0xD0).Concat(data16).ToArray()),

                new("WriteSetting(Usart1BaudRate, 0x12 0x34)", QyBoard.WriteSetting(QyBoard.Setting.Usart1BaudRate, 0x12, 0x34), B(0xE6, 0x12, 0x34)),
                new("ReadSettings()", QyBoard.ReadSettings(), B(0xF0)),
                new("EndOfPacket()", QyBoard.EndOfPacket(), B(0xFF)),
                new("EndOfPacket(1)", QyBoard.EndOfPacket(1), B(0xF1)),

                new("Sleep()", QyBoard.Sleep(), B(0xE0)),
                new("DeviceToHostStreaming(sel=AA, timer=1234)", QyBoard.DeviceToHostStreaming(0xAA, 0x1234), B(0xE1, 0xAA, 0x12, 0x34)),
                new("HostToDeviceStreaming(11 22)", QyBoard.HostToDeviceStreaming(0x11, 0x22), B(0xE2, 0x11, 0x22)),
                new("StoreCurrentOutputs()", QyBoard.StoreCurrentOutputs(), B(0xEC)),
                new("WatchDogTimer(true)", QyBoard.WatchDogTimer(true), B(0xED, 0x01)),
                new("WatchDogTimer(false)", QyBoard.WatchDogTimer(false), B(0xED, 0x00)),
                new("SaveSettings()", QyBoard.SaveSettings(), B(0xEE)),
                new("Reset()", QyBoard.Reset(), B(0xEF)),
            };
        }

        private static void PrintCase(Case c, bool ok)
        {
            var status = ok ? "PASS" : "FAIL";

            Console.WriteLine($"{status}  {c.Name}");
            Console.WriteLine($"  actual:   {Hex(c.Actual)}");
            Console.WriteLine($"  expected: {Hex(c.Expected)}");

            if (!ok)
            {
                // Minimal diff marker (same length? mark mismatches)
                Console.WriteLine($"  diff:     {DiffLine(c.Expected, c.Actual)}");
            }

            Console.WriteLine();
        }

        private static string Hex(byte[] data)
            => string.Join(" ", data.Select(b => b.ToString("X2")));

        private static string DiffLine(byte[] expected, byte[] actual)
        {
            int n = Math.Max(expected.Length, actual.Length);
            var parts = new string[n];

            for (int i = 0; i < n; i++)
            {
                bool hasE = i < expected.Length;
                bool hasA = i < actual.Length;

                if (!hasE) parts[i] = "??";         // extra actual byte(s)
                else if (!hasA) parts[i] = "!!";    // missing actual byte(s)
                else parts[i] = expected[i] == actual[i] ? ".." : "^^";
            }

            return string.Join(" ", parts);
        }
    }
}