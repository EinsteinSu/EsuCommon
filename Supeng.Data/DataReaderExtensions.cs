using System;
using System.Data;

namespace Supeng.Data
{
  public static class DataReaderExtensions
  {
    public static bool HasColumn(this IDataReader reader, string columnName)
    {
      for (int i = 0; i < reader.FieldCount; i++)
      {
        if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      return false;
    }
  }
}
