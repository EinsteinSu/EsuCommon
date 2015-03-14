using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Data.Sql.MsTest
{
  [TestClass]
  public class SqlStorageTest : DataTestBase
  {
    
    [TestInitialize]
    public void Init()
    {
      string sql = "INSERT INTO TestTable1(id,name) values(1,'name1');INSERT INTO TestTable1(id,name) values(2,'name2');";
      Storage.Execute(sql);
    }
    [TestMethod]
    public void TestReadData()
    {
      var list = new List<string>();
      Storage.ReadData("Select * from TestTable1", reader => list.Add(reader["name"].ToString()));
      Assert.IsTrue(list.Count == 2);
    }

    [TestCleanup]
    public void Clean()
    {
      string sql = "Delete from TestTable1";
      Storage.Execute(sql);
    }
  }
}
