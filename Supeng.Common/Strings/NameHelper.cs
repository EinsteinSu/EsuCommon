#region

using System.Collections.Generic;

#endregion

namespace Supeng.Common.Strings
{
  public static class NameHelper
  {
    private static readonly IList<string> CompoundNames = new[] {"欧阳", "慕容"};

    public static string GetFirstName(this string name)
    {
      if (string.IsNullOrEmpty(name))
        return string.Empty;
      foreach (var compoundName in CompoundNames)
      {
        if (name.StartsWith(compoundName))
          return compoundName;
      }
      return name.Substring(0, 1);
    }
  }
}