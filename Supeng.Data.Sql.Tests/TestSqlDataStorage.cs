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
      var storage = new SqlDataStorage<ParameterInfo<string>>(ConnectionString);
      string guid = Guid.NewGuid().ToString();
      string sql = string.Format("Insert into parameter(id) values('{0}')", guid);
      int result = storage.Execute(sql, new TestBackgroudData(1));
      Assert.AreEqual(1, result);
      sql = string.Format("Delete from parameter where id = '{0}'", guid);
      result = storage.Execute(sql, new TestBackgroudData(1));
      Assert.AreEqual(1, result);
    }

  
  }
}
