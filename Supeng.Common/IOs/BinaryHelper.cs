using System.IO;

namespace Supeng.Common.IOs
{
  public static class BinaryHelper
  {
    public static byte[] FileToByte(this string fileName)
    {
      using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        var data = new byte[fs.Length];
        fs.Read(data, 0, (int) fs.Length);
        return data;
      }
    }

    public static void ByteToFile(this byte[] data, string fileName)
    {
      using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
      {
        fs.Write(data, 0, data.Length);
      }
    }
  }
}