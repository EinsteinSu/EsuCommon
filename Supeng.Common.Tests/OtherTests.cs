using System;
using System.Text;
using NUnit.Framework;
using Supeng.Common.Strings;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class OtherTests
  {
    public static string ByteArrayToHexString(byte[] Bytes)
    {
      var Result = new StringBuilder(Bytes.Length*2);
      string HexAlphabet = "0123456789ABCDEF";

      foreach (byte B in Bytes)
      {
        Result.Append(HexAlphabet[B >> 4]);
        Result.Append(HexAlphabet[B & 0xF]);
      }

      return Result.ToString();
    }

    public static byte[] HexStringToByteArray(string Hex)
    {
      var Bytes = new byte[Hex.Length/2];
      int[] HexValue =
      {
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
        0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
      };

      for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
      {
        Bytes[x] = (byte) (HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                           HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
      }

      return Bytes;
    }

    [Test]
    public void TestBinary()
    {
      byte[] data = {1, 2, 4, 8, 16, 32};
      string text = Encoding.UTF8.GetString(data);
      byte[] data1 = Encoding.UTF8.GetBytes(text);
      CollectionAssert.AreEqual(data, data1);
    }

    [Test]
    public void TestPascal()
    {
      string name = "test".GetPascalName();
      Assert.AreEqual("Test", name);
    }
  }
}