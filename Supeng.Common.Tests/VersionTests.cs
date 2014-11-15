using NUnit.Framework;
using Version = Supeng.Common.Entities.EsuVersion;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class VersionTests
  {
    [Test]
    public void TestVersionCompare()
    {
      var v1 = new Version(1, 2, 3);
      var v2 = new Version(1, 2, 2);
      var v3 = new Version(1, 2, 3);
      Assert.IsTrue(v1 > v2);
      Assert.IsTrue(v2 < v1);
      Assert.IsTrue(v1 != v2);
      Assert.IsTrue(v1 == v3);
    }

    [Test]
    public void TestVersionConvert()
    {
      var v1 = "1.2.3";
      Version v2 = v1;
      Assert.IsTrue(v1 == v2);
    }
  }
}
