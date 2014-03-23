using NUnit.Framework;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class EsuInfoCollectionTests
  {
    [Test]
    public void TestDataChange()
    {
      var collection = new EsuInfoCollection<TestData>
      {
        EsuInfoBase.InitailizeDefaultData<TestData>(),
        EsuInfoBase.InitailizeDefaultData<TestData>()
      };

      Assert.IsTrue(collection.HasChanged);
      Assert.AreEqual(collection.ChangedCollection.Count, 2);

      collection.Remove(collection[0]);
      Assert.AreEqual(collection.ChangedCollection.Count, 2);

      collection[0].Description = "Test1";
      Assert.AreEqual(collection.ChangedCollection.Count, 2);

      collection.AcceptChanges();
      Assert.IsFalse(collection.HasChanged);

      var data = collection[0];
      data.Description = "Test2";
      
      Assert.AreEqual(collection.ChangedCollection.Count, 1);

    }
  }

  internal sealed class TestData : EsuInfoBase
  {

  }
}
