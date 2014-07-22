using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Data.DataFilter
{
  public class FilterCollection : EsuInfoCollection<FilterModel>
  {
    public virtual string GetSql(string tableName, string columns = "*", string order = "")
    {
      var creator = new SqlScriptCreator(tableName, new[] {CurrentItem.Filter}, columns);
      return creator.GetSqlScript(order);
    }
  }
}