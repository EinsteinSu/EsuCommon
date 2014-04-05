using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Supeng.Common.Types;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class DataConvertTests
  {
    [Test]
    public void TestDataConvert()
    {
      string data = "1";
      int i = data.ConvertData<int>();
      Assert.AreEqual(1, i);

      decimal d = data.ConvertData<decimal>();
      Assert.AreEqual(1, d);

      //DateTime time = DateTime.Now;
      //data = time.ToString(CultureInfo.InvariantCulture);

      //var result = data.ConvertData<DateTime>();
      //Assert.AreEqual(time, result);
    }
  }
}
