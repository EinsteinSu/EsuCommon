using System;
using System.Collections.Generic;

namespace Supeng.Data
{
  public static class SqlScriptHelper
  {
    /// <summary>
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

    public static string GetDatetimeFilter(string columnName, DateTime start, DateTime end)
    {
      return string.Format("{0} BETWEEN '{1} 00:00:00' AND '{2} 23:59:59'", columnName, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
    }

    public static IList<string> GetFilterArray(this IDictionary<string, string> list)
    {
      var conditions = new List<string>();
      foreach (var column in list)
      {
        if (!string.IsNullOrEmpty(column.Value))
          conditions.Add(string.Format("{0} Like '%{1}%'", column.Key, column.Value));
      }
      return conditions;
    }
  }
}