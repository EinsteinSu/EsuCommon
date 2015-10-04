using System.Globalization;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Wpf.Common.UnitTest
{
  internal class TestData : EsuInfoBase
  {
    public string Name { get; set; }

    public static EsuInfoCollection<TestData> GetTestDataCollection(int count)
    {
      var collection = new EsuInfoCollection<TestData>();
      for (int i = 0; i < count; i++)
      {
        collection.Add(new TestData { ID = i.ToString(CultureInfo.InvariantCulture), Name = "Name:" + i });
      }
      return collection;
    }
  }
}