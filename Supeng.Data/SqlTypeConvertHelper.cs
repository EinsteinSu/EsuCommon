namespace Supeng.Data
{
  public static class SqlTypeConvertHelper
  {
    public static string ConvertToSystemType(this string sqlType)
    {
      if (sqlType.Contains("char"))
        return "string";
      if (sqlType.Contains("date") || sqlType.Contains("time"))
        return "DateTime";
      if (sqlType.Contains("int"))
        return "int";
      if (sqlType.Contains("decimal"))
        return "decimal";
      return string.Empty;
    }
  }
}