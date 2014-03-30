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
  }
}