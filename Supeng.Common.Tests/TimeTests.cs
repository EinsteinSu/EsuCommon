using System;
using NUnit.Framework;
using Supeng.Common.Entities.BasesEntities;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class TimeTests
  {
    [Test]
    public void TestSubTime()
    {
      var time1 = new DateTime(2001, 1, 1, 12, 10, 0);
      var time2 = new DateTime(2001, 1, 1, 12, 15, 0);
      var file1 = new UpdateFile {LastWriteTime = time1};
      var file2 = new UpdateFile {LastWriteTime = time2};
      Assert.IsTrue(file1 < file2);
      Console.WriteLine(time1.Subtract(time2).TotalSeconds);
    }
  }
}