using System;
using System.Collections.Generic;
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
       decimal data = (decimal) 11234115.12D;
      Console.WriteLine(data.GetJedx());
    }
  }
}
