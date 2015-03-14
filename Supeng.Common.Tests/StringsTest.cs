using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Supeng.Common.Strings;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class StringsTest
  {
    [Test]
    public void TestGetFiles()
    {
      string fileName = @"C:\Users\Einstein\Desktop\EsuCommon\Update";
      foreach (string str in Directory.GetFiles(fileName, "*", SearchOption.AllDirectories))
      {
        Console.WriteLine(str);
      }
    }

    [Test]
    public void TestJedx()
    {
      var data = (decimal)11234115.12D;
      Console.WriteLine(data.GetJedx());
    }

    [Test]
    public void TestStringBuild()
    {
      const string data = "Test{0},{1}";
      var sb = new StringBuilder();
      sb.EsuAppendFormat(data, "1", "2");
      Assert.AreEqual("Test1,2" + Environment.NewLine, sb.ToString());
    }

    [Test]
    public void GetPassword()
    {
      string data = "VjMgLJhGdw0=";
      var security = new StringSecurity();
      var password = security.DecryptString(data);
      Console.WriteLine(password);
    }
  }
}