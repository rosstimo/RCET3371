// File: QyAt.Tests/QyBoardTests.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using QyAt;

namespace QyAt.Tests
{
    public class QyBoardTests
    {
        // ---------- helpers ----------
        private static byte[] Bytes(params byte[] b) => b;

        private static byte[] Seq(int startInclusive, int count)
            => Enumerable.Range(startInclusive, count).Select(i => (byte)i).ToArray();

        private static string Hex(byte[] data)
            => string.Join(" ", data.Select(x => x.ToString("X2")));

        private static void AssertBytes(byte[] expected, byte[] actual)
        {
            Assert.True(expected.SequenceEqual(actual),
                $"Expected: {Hex(expected)}{Environment.NewLine}Actual:   {Hex(actual)}");
        }

        // ---------- Core helper tests ----------

        [Fact]
        public void BuildCommandByte_Basic()
        {
            Assert.Equal(0xA5, QyBoard.BuildCommandByte(0xA, 0x5));
            Assert.Equal(0x10, QyBoard.BuildCommandByte(0x1, 0x0));
            Assert.Equal(0xFF, QyBoard.BuildCommandByte(0x1F, 0x2F)); // masks to nibble
        }

        [Fact]
        public void Concat_JoinsArrays()
        {
            var a = Bytes(0x01, 0x02);
            var b = Bytes(0xAA);
            var c = Bytes(0x10, 0x20, 0x30);

            var joined = QyBoard.Concat(a, b, c);
            AssertBytes(Bytes(0x01, 0x02, 0xAA, 0x10, 0x20, 0x30), joined);
        }

        // ---------- Command tests 0-15 ----------

        [Fact]
        public void Null_Is00()
            => AssertBytes(Bytes(0x00), QyBoard.Null());

        [Fact]
        public void ReadStatus_Is10()
            => AssertBytes(Bytes(0x10), QyBoard.ReadStatus());

        [Fact]
        public void WriteDigitalOutputs_Is20PlusData()
            => AssertBytes(Bytes(0x20, 0xA5), QyBoard.WriteDigitalOutputs(0xA5));

        [Fact]
        public void ReadDigitalInputs_Is30()
            => AssertBytes(Bytes(0x30), QyBoard.ReadDigitalInputs());

        [Theory]
        [InlineData(0, 0,    0x40, 0x00, 0x00)]
        [InlineData(2, 512,  0x42, 0x80, 0x00)] // 512 -> 0b10_0000_0000 => high=0x80, low=0x00
        [InlineData(15, 1023,0x4F, 0xFF, 0xC0)] // max 10-bit -> high=255, low bits 11 -> 0xC0
        public void WriteAnalogOutput_Encodes10Bit(byte channel, ushort value, byte cmd, byte hi, byte lo)
        {
            var actual = QyBoard.WriteAnalogOutput(channel, value);
            AssertBytes(Bytes(cmd, hi, lo), actual);
        }

        [Fact]
        public void WriteAnalogOutput_ChannelOutOfNibble_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.WriteAnalogOutput(16, 0));

        [Fact]
        public void WriteAnalogOutput_ValueOutOfRange_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.WriteAnalogOutput(0, 1024));

        [Fact]
        public void WriteAnalogOutputRaw_Passthrough()
        {
            var actual = QyBoard.WriteAnalogOutputRaw(channel: 2, highByte: 0x12, lowByte: 0x34);
            AssertBytes(Bytes(0x42, 0x12, 0x34), actual);
        }

        [Fact]
        public void ReadAnalogInputs_RejectsZeroMask()
            => Assert.Throws<ArgumentException>(() => QyBoard.ReadAnalogInputs(AnalogInputMask.None));

        [Fact]
        public void ReadAnalogInputs_A1A4Mask()
        {
            var actual = QyBoard.ReadAnalogInputs(AnalogInputMask.A1 | AnalogInputMask.A4);
            AssertBytes(Bytes(0x59), actual); // cmd=0x5, arg=0b1001 => 0x59
        }

        [Fact]
        public void WriteUsart1_Length1()
        {
            var actual = QyBoard.WriteUsart1(0x01);
            AssertBytes(Bytes(0x61, 0x01), actual);
        }

        [Fact]
        public void WriteUsart1_Length15()
        {
            var data = Seq(1, 15);
            var actual = QyBoard.WriteUsart1(data);
            Assert.Equal(16, actual.Length);
            Assert.Equal(0x6F, actual[0]);
            AssertBytes(Bytes(0x6F).Concat(data).ToArray(), actual);
        }

        [Fact]
        public void WriteUsart1_Length0_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.WriteUsart1(Array.Empty<byte>()));

        [Fact]
        public void WriteUsart1_Length16_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.WriteUsart1(Seq(0, 16)));

        [Fact]
        public void ReadUsart1Buffer_Is70()
            => AssertBytes(Bytes(0x70), QyBoard.ReadUsart1Buffer());

        [Fact]
        public void WriteUsart2_Length3()
        {
            var actual = QyBoard.WriteUsart2(0xAA, 0xBB, 0xCC);
            AssertBytes(Bytes(0x83, 0xAA, 0xBB, 0xCC), actual);
        }

        [Fact]
        public void ReadUsart2Buffer_Is90()
            => AssertBytes(Bytes(0x90), QyBoard.ReadUsart2Buffer());

        [Fact]
        public void WriteSpi_Length2()
        {
            var actual = QyBoard.WriteSpi(0xDE, 0xAD);
            AssertBytes(Bytes(0xA2, 0xDE, 0xAD), actual);
        }

        [Fact]
        public void ReadSpiBuffer_IsB0()
            => AssertBytes(Bytes(0xB0), QyBoard.ReadSpiBuffer());

        [Fact]
        public void I2cWrite_BuildsAddressByte_Write()
        {
            // 7-bit address 0x50 -> address byte 0xA0, RW=0
            var actual = QyBoard.I2cWrite(0x50, 0xAA, 0xBB, 0xCC);
            AssertBytes(Bytes(0xC3, 0xA0, 0xAA, 0xBB, 0xCC), actual);
        }

        [Fact]
        public void I2cRead_BuildsAddressByte_Read()
        {
            // 7-bit address 0x50 -> address byte 0xA1, RW=1
            var actual = QyBoard.I2cRead(0x50, 4);
            AssertBytes(Bytes(0xC4, 0xA1), actual);
        }

        [Fact]
        public void I2cTransaction_Count16_EncodesNibbleAs0()
        {
            // count=16 -> arg nibble 0
            var data16 = Seq(0x10, 16);
            var actual = QyBoard.I2cTransaction(addressByteWithRw: 0xA0, count: 16, writeData: data16);
            Assert.Equal(18, actual.Length);
            Assert.Equal(0xC0, actual[0]);
            Assert.Equal(0xA0, actual[1]);
            AssertBytes(Bytes(0xC0, 0xA0).Concat(data16).ToArray(), actual);
        }

        [Fact]
        public void I2cWrite_AddressOutOfRange_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.I2cWrite(0x80, 0x01));

        [Fact]
        public void I2cRead_CountOutOfRange_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.I2cRead(0x50, 0));

        [Fact]
        public void SdTransaction_Length1()
        {
            var actual = QyBoard.SdTransaction(0x99);
            AssertBytes(Bytes(0xD1, 0x99), actual);
        }

        [Fact]
        public void SdTransaction_Length16_EncodesNibbleAs0()
        {
            var data16 = Seq(0x20, 16);
            var actual = QyBoard.SdTransaction(data16);
            Assert.Equal(17, actual.Length);
            Assert.Equal(0xD0, actual[0]);
            AssertBytes(Bytes(0xD0).Concat(data16).ToArray(), actual);
        }

        [Fact]
        public void WriteSetting_ZeroTo4Bytes()
        {
            AssertBytes(Bytes(0xE6, 0x12, 0x34), QyBoard.WriteSetting(QyBoard.Setting.Usart1BaudRate, 0x12, 0x34));
            AssertBytes(Bytes(0xEE), QyBoard.WriteSetting(QyBoard.Setting.SaveSettings));
        }

        [Fact]
        public void WriteSetting_TooManyBytes_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.WriteSetting(1, 1, 2, 3, 4, 5));

        [Fact]
        public void ReadSettings_IsF0()
            => AssertBytes(Bytes(0xF0), QyBoard.ReadSettings());

        [Fact]
        public void EndOfPacket_DefaultIsFF()
            => AssertBytes(Bytes(0xFF), QyBoard.EndOfPacket());

        [Fact]
        public void EndOfPacket_Argument0_Throws()
            => Assert.Throws<ArgumentOutOfRangeException>(() => QyBoard.EndOfPacket(0));

        [Fact]
        public void EndOfPacket_Argument1_IsF1()
            => AssertBytes(Bytes(0xF1), QyBoard.EndOfPacket(1));

        // ---------- Setup/Settings helper tests ----------

        [Fact]
        public void Sleep_IsE0()
            => AssertBytes(Bytes(0xE0), QyBoard.Sleep());

        [Fact]
        public void DeviceToHostStreaming_EncodesTimer()
        {
            var actual = QyBoard.DeviceToHostStreaming(selectionBits: 0xAA, timer: 0x1234);
            AssertBytes(Bytes(0xE1, 0xAA, 0x12, 0x34), actual);
        }

        [Fact]
        public void HostToDeviceStreaming_TwoSelectionBytes()
        {
            var actual = QyBoard.HostToDeviceStreaming(0x11, 0x22);
            AssertBytes(Bytes(0xE2, 0x11, 0x22), actual);
        }

        [Fact]
        public void AnalogOutputOptions_IsE4PlusOneByte()
            => AssertBytes(Bytes(0xE4, 0x77), QyBoard.AnalogOutputOptions(0x77));

        [Fact]
        public void DigitalOutputOptions_IsE5PlusTwoBytes()
            => AssertBytes(Bytes(0xE5, 0x01, 0x02), QyBoard.DigitalOutputOptions(0x01, 0x02));

        [Fact]
        public void Usart1BaudRate_IsE6PlusTwoBytes()
            => AssertBytes(Bytes(0xE6, 0x12, 0x34), QyBoard.Usart1BaudRate(0x1234));

        [Fact]
        public void Usart2Config_IsE7PlusFourBytes()
        {
            var actual = QyBoard.Usart2Config(baudRegisterValue: 0x1234, rcPinLocation: 0x05, txPinLocation: 0x06);
            AssertBytes(Bytes(0xE7, 0x12, 0x34, 0x05, 0x06), actual);
        }

        [Fact]
        public void SpiBaudRate_IsE8PlusTwoBytes()
            => AssertBytes(Bytes(0xE8, 0xAA, 0xBB), QyBoard.SpiBaudRate(0xAA, 0xBB));

        [Fact]
        public void I2cBaudRateOrDisable_IsE9PlusOneByte()
            => AssertBytes(Bytes(0xE9, 0x33), QyBoard.I2cBaudRateOrDisable(0x33));

        [Fact]
        public void StoreCurrentOutputs_IsEC()
            => AssertBytes(Bytes(0xEC), QyBoard.StoreCurrentOutputs());

        [Fact]
        public void WatchDogTimer_EnableDisable()
        {
            AssertBytes(Bytes(0xED, 0x01), QyBoard.WatchDogTimer(true));
            AssertBytes(Bytes(0xED, 0x00), QyBoard.WatchDogTimer(false));
        }

        [Fact]
        public void SaveSettings_IsEE()
            => AssertBytes(Bytes(0xEE), QyBoard.SaveSettings());

        [Fact]
        public void Reset_IsEF()
            => AssertBytes(Bytes(0xEF), QyBoard.Reset());

        // ---------- Decode helper ----------
        [Fact]
        public void Decode10Bit_Works()
        {
            // value 513 => high=0x80, low bits=01 -> lowByte has bits7..6 = 01 => 0x40
            Assert.Equal((ushort)513, QyBoard.Decode10Bit(0x80, 0x40));
            Assert.Equal((ushort)1023, QyBoard.Decode10Bit(0xFF, 0xC0));
            Assert.Equal((ushort)0, QyBoard.Decode10Bit(0x00, 0x00));
        }
    }
}