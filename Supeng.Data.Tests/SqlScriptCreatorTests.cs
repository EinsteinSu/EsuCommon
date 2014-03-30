using System.Collections.Generic;
using NUnit.Framework;

namespace Supeng.Data.Tests
{
  [TestFixture]
  public class SqlScriptCreatorTests
  {
    [Test]
    public void GetSqlScriptTest()
    {
      var whereConditions = new List<string> {"0=0", "0=1", "0=2"};
      SqlScriptCreator sql = new SqlScriptCreator("Table1", whereConditions);
      string assertSql = "Select * from Table1 where 0=0 and 0=1 and 0=2";
      Assert.AreEqual(sql.GetSqlScript().ToUpper(), assertSql.ToUpper(), "Where condition has issue.");

      sql = new SqlScriptCreator("Table1");
      assertSql = "Select * from Table1";
      Assert.AreEqual(sql.GetSqlScript().ToUpper(), assertSql.ToUpper(), "Select script has issue.");
    }
  }
}
