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
  public class OtherTests
  {
    [Test]
    public void TestPascal()
    {
      string name = "test".GetPascalName();
      Assert.AreEqual("Test", name);
    }
  }
}
