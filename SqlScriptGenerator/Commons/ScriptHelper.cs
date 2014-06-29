namespace SqlScriptGenerator.Commons
{
  public static class ScriptHelper
  {
    public static string ReplaceBracket(this string data)
    {
      return data.Replace("【", "{").Replace("】", "}");
    }

    public static string QuoteSequnce(this int sequnce, string dataType)
    {
      if (dataType.Contains("char") || dataType.Contains("time"))
        return string.Format("'【{0}】'", sequnce).ReplaceBracket();
      return string.Format("【{0}】", sequnce).ReplaceBracket();
    }
  }
}