using System;
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
      var whereConditions = new List<string> { "0=0", "0=1", "0=2" };
      SqlScriptCreator sql = new SqlScriptCreator("Table1", whereConditions);
      string assertSql = "Select * from Table1 where 0=0 and 0=1 and 0=2";
      Assert.AreEqual(sql.GetSqlScript().ToUpper(), assertSql.ToUpper(), "Where condition has issue.");

      sql = new SqlScriptCreator("Table1");
      assertSql = "Select * from Table1";
      Assert.AreEqual(sql.GetSqlScript().ToUpper(), assertSql.ToUpper(), "Select script has issue.");
    }

    [Test]
    public void TestFilterCreator()
    {
      string result = "Where 0=0 And Column1 Like '%Test%'";
      var list = new Dictionary<string, string>();
      list.Add("Column1", "Test");
      list.Add("Column2", "");
      var text = list.GetFilter();
      Console.WriteLine(text);
      Assert.AreEqual(result, text);
    }
  }
}
