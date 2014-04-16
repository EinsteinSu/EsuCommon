using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Types;
using Supeng.Data;
using Supeng.Data.Sql;

namespace Supeng.Common.Tests
{
  [TestFixture]
  public class DataSaveTests : TestConnection
  {
    [Test]
    public void TestDataSave()
    {
      var collection = new EsuInfoCollection<TestData>();
      for (int i = 0; i < 10; i++)
      {
        collection.Add(new TestData { ID = Guid.NewGuid().ToString(), Name = "User" + i, Age = 10 + i });
      }
      collection.Save(new TestDataStorage(Connection), new SaveTest());
      var databaseCollection = new TestDataStorage(Connection).GetDataCollection();
      Assert.AreEqual(10, databaseCollection.Count);

      foreach (var testData in collection)
      {
        testData.Name = "test";
      }
      collection.Save(new TestDataStorage(Connection), new SaveTest());
      databaseCollection = new TestDataStorage(Connection).GetDataCollection();

      foreach (var testData in databaseCollection)
      {
        Assert.AreEqual(testData.Name, "test");
      }

      collection.RemoveAll();
      collection.Save(new TestDataStorage(Connection), new SaveTest());
      databaseCollection = new TestDataStorage(Connection).GetDataCollection();
      Assert.AreEqual(0, databaseCollection.Count);
    }

    [Test]
    public void TestSaveSingleRecord()
    {
      string guid = Guid.NewGuid().ToString();
      var test = new TestData { ID = guid, Name = "Test", Age = 19 };
      test.Insert(new TestDataStorage(Connection), new SaveTest());
      var collection = new TestDataStorage(Connection).GetDataCollection();
      Assert.IsTrue(collection.Any());

      test.Name = "test1";
      test.Update(new TestDataStorage(Connection), new SaveTest());
      collection = new TestDataStorage(Connection).GetDataCollection();
      Assert.AreEqual(test.Name, collection[0].Name);

      test.Delete(new TestDataStorage(Connection), new SaveTest());
      collection = new TestDataStorage(Connection).GetDataCollection();
      Assert.IsFalse(collection.Any());
    }
  }

  public class TestDataStorage : SqlDataStorage, IDataCreator<TestData>
  {
    public TestDataStorage(string connectionString)
      : base(connectionString, TaskScheduler.Current)
    {
    }

    public EsuInfoCollection<TestData> GetDataCollection()
    {
      var creator = new SqlScriptCreator("SaveTable");
      return ReadToCollection(creator.GetSqlScript(), new TestDataStorage(""), null, null);
    }

    public TestData CreateData(IDataReader reader)
    {
      return new TestData
      {
        ID = reader["ID"].ToString(),
        Name = reader["Name"].ToString(),
        Age = reader["Age"].ToString().ConvertData(0)
      };
    }
  }

  public class TestData : EsuInfoBase
  {
    private string name;
    private int age;

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public int Age
    {
      get { return age; }
      set
      {
        if (value == age) return;
        age = value;
        NotifyOfPropertyChange(() => Age);
      }
    }
  }

  public class SaveTest : IDataSave<TestData>
  {
    public string InsertSqlScript(TestData data)
    {
      return string.Format("Insert into SaveTable(ID,Name,Age) values('{0}','{1}',{2})", data.ID, data.Name, data.Age);
    }

    public string UpdateSqlScript(TestData data)
    {
      return string.Format("Update SaveTable Set Name = '{0}',Age = {1} Where ID = '{2}'", data.Name, data.Age, data.ID);
    }

    public string DeleteSqlScript(TestData data)
    {
      return string.Format("Delete from SaveTable Where ID = '{0}'", data.ID);
    }
  }
}
