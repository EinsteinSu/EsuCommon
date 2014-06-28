using System;
using System.Collections.Generic;
using System.Linq;

namespace Supeng.Data
{
  public class SqlScriptCreator
  {
    private readonly string columns;
    private readonly string tableName;
    private readonly IList<string> whereCondition;

    public SqlScriptCreator(string tableName, IList<string> whereCondition = null, string columns = "*")
    {
      this.tableName = tableName;
      this.columns = columns;
      this.whereCondition = whereCondition;
    }

    public string GetSqlScript()
    {
      if (whereCondition != null && whereCondition.Any())
      {
        string conditions = string.Empty;
        for (int i = 0; i < whereCondition.Count(); i++)
        {
          if (i < whereCondition.Count() - 1)
            conditions += string.Format(" {0} And", whereCondition[i]);
          else
          {
            conditions += " " + whereCondition[i];
          }
        }
        return string.Format("Select {0} from {1} Where{2}", columns, tableName, conditions);
      }
      return string.Format("Select {0} from {1}", columns, tableName);
    }

    public string GetSqlScript(string order)
    {
      return string.Format("{0} {1}", GetSqlScript(), order);
    }
  }


  public static class SqlScriptHelper
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="list">key is column value is column value</param>
    /// <returns></returns>
    public static string GetFilter(this IDictionary<string, string> list)
    {
      string filter = "0=0";
      foreach (var column in list)
      {
        if (!string.IsNullOrEmpty(column.Value))
          filter += string.Format(" And {0} Like '%{1}%'", column.Key, column.Value);
      }
      return filter;
    }
  }
}