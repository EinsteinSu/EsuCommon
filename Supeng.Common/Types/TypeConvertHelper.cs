using System;
using System.Linq;
using System.Reflection;

namespace Supeng.Common.Types
{
  public static class TypeConvertHelper
  {
    public static T ConvertData<T>(this string data, T defaultValue = default (T))
    {
      if (string.IsNullOrEmpty(data))
        return defaultValue;
      MethodInfo memberInfo = typeof (T).GetMethod("TryParse", new[] {data.GetType(), typeof (T).MakeByRefType()});
      if (memberInfo != null)
      {
        bool b;
        object[] parameters = {data, null};
        bool.TryParse(memberInfo.Invoke(null, parameters).ToString(), out b);
        if (b && parameters.Count() > 1)
          return (T) parameters[1];
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

    public static string ConvertToCnNumber(this int num)
    {
      switch (num)
      {
        case 1:
          return "一";
        case 2:
          return "二";
        case 3:
          return "三";
        case 4:
          return "四";
        case 5:
          return "五";
        case 6:
          return "六";
        case 7:
          return "七";
        case 8:
          return "八";
        case 9:
          return "九";
        case 10:
          return "十";
      }
      return string.Empty;
    }

    public static string ConvertToCnString(this bool data)
    {
      return data ? "是" : "否";
    }
  }
}