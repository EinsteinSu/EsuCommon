using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

    [Test]
    public void TestParameterWithInsert()
    {
      string sql = "Insert into [user](name) values(@name)";
      IDataParameter[] parameters = new IDataParameter[3];
      parameters[0] = new SqlParameter("@id",1);
      parameters[1]  = new SqlParameter("@name","Test");
      SqlConnection connection = new SqlConnection(ConnectionString);
      connection.Open();
      SqlCommand command = new SqlCommand(sql,connection);
      command.Parameters.AddWithValue("@id", 1);
      command.Parameters.AddWithValue("@name", "Test");
      command.ExecuteNonQuery();
      connection.Close();
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
