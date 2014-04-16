using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Supeng.Common.Entities.BasesEntities.DataEntities;
using Test.Common;
[assembly: RequiresThread]

namespace Supeng.Data.Sql.Tests
{
  [TestFixture]
  public class TestSqlDataStorage : ConnectionTest
  {
    [Test]
    public void TestExecuteSql()
    {
      var storage = new SqlDataStorage(ConnectionString);
      string guid = Guid.NewGuid().ToString();
      string sql = string.Format("Insert into parameter(id) values('{0}')", guid);
      int result = storage.Execute(sql, exceptionHandle: new TestBackgroudData(1));
      Assert.AreEqual(1, result);
      sql = string.Format("Delete from parameter where id = '{0}'", guid);
      result = storage.Execute(sql, exceptionHandle: new TestBackgroudData(1));
      Assert.AreEqual(1, result);
    }

    [Test, RequiresThread]
    public void TestWithAPMCancel()
    {
      var storage = new SqlDataStorage(ConnectionString);
      string sql = "Select * from parameter";
      storage.ReadToCollectionWithAPM(sql,new ParameterInfoCreator<string>(),null, new TestBackgroudCollection<ParameterInfo<string>>());
      storage.Cancellation.Cancel();
    }
  }
}
