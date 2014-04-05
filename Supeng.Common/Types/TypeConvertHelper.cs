using System;
using System.Linq;

namespace Supeng.Common.Types
{
  public static class TypeConvertHelper
  {
    public static T ConvertData<T>(this string data, T defaultValue = default (T))
    {
      if (string.IsNullOrEmpty(data))
        return defaultValue;
      var memberInfo = typeof(T).GetMethod("TryParse", new[] { data.GetType(), typeof(T).MakeByRefType() });
      if (memberInfo != null)
      {
        bool b;
        object[] parameters = { data, null };
        bool.TryParse(memberInfo.Invoke(null, parameters).ToString(), out b);
        if (b && parameters.Count() > 1)
          return (T)parameters[1];
      }
      return defaultValue;
    }

    public static T EnumConvert<T>(this string data) where T : struct
    {
      T e;
      Enum.TryParse(data, out e);
      return e;
    }

    public static DateTime ConvertToDateTime(this string data)
    {
      return data.ConvertData(new DateTime(1900, 1, 1));
    }
  }
}
