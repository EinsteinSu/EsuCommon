using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Supeng.Common.Strings;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class StringsTest
  {
    [Test]
    public void TestStringBuild()
    {
      string data = "Test{0},{1}";
      StringBuilder sb = new StringBuilder();
      sb.EsuAppendFormat(data, "1", "2");
      Assert.AreEqual("Test1,2" + Environment.NewLine, sb.ToString());
    }

    [Test]
    public void TestJedx()
    {
      decimal data = (decimal)11234115.12D;
      Console.WriteLine(data.GetJedx());
    }

    [Test]
    public void TestGetFiles()
    {
      string fileName = @"C:\Users\Einstein\Desktop\EsuCommon\Update";
      foreach (var str in Directory.GetFiles(fileName, "*", SearchOption.AllDirectories))
      {
        Console.WriteLine(str);
      }
    }
  }
}
