using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Types;

namespace Supeng.Common.Strings
{
  public static class StringExtensions
  {
    public const char Split = '△';

    public static string GetJedx(this decimal je)
    {
      string dx = "零元";
      if (je > 0)
      {
        try
        {
          string s = je.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
          string d = Regex.Replace(s,
            @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))",
            "${b}${z}");
          dx = Regex.Replace(d, ".",
            m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString(CultureInfo.InvariantCulture));
        }
        catch
        {
          return dx;
        }
      }
      return dx;
    }

    public static T Load<T>(this string data)
    {
      return JsonConvert.DeserializeObject<T>(data);
    }

    public static string BytesToString(this byte[] data)
    {
      return Encoding.UTF8.GetString(data);
    }

    public static byte[] StringToBytes(this string data)
    {
      return Encoding.UTF8.GetBytes(data);
    }

    public static string GetPascalName(this string name)
    {
      if (!String.IsNullOrEmpty(name))
      {
        string first = name.Substring(0, 1);
        string other = name.Substring(1, name.Length - 1);
        return String.Format("{0}{1}", first.ToUpper(), other);
      }
      return String.Empty;
    }

      public static string GetLowerCaseName(this string name)
      {
          if (!String.IsNullOrEmpty(name))
          {
              string first = name.Substring(0, 1);
              string other = name.Substring(1, name.Length - 1);
              return String.Format("{0}{1}", first.ToLower(), other);
          }
          return String.Empty;
      }

    public static string SplitToString(this IList<string> collection, char c)
    {
      return collection.Aggregate(String.Empty, (current, s) => current + String.Format("{0}{1}", s, c)).TrimEnd(c);
    }

    public static List<string> GetStringCollection(this string str, char c)
    {
      return str.Split(c).ToList();
    }

    public static void EsuAppendFormat(this StringBuilder sb, string data, params object[] strs)
    {
      sb.AppendLine(String.Format(data, strs));
    }

    public static string Package(this IDictionary<string, string> dictionary)
    {
      var sb = new StringBuilder(dictionary.Count * 2);
      foreach (var valuePair in dictionary)
      {
        sb.AppendFormat("{0}{1}{2}{1}", valuePair.Key, Split, valuePair.Value);
      }
      return sb.ToString().TrimEnd(Split);
    }

    public static IDictionary<string, string> UnPackedToDictionary(this string data)
    {
      var dictionary = new Dictionary<string, string>();
      string[] datas = data.Split(Split);
      for (int i = 0; i < datas.Length; i++)
      {
        if (i % 2 == 0)
        {
          dictionary.Add(datas[i], "");
        }
        else
        {
          dictionary[datas[i - 1]] = datas[i];
        }
      }
      return dictionary;
    }

    public static string PackageChangedData<T>(this EsuInfoCollection<T> collection) where T : EsuInfoBase, new()
    {
      var data = new Dictionary<string, string>();
      foreach (ChangeData<T> change in collection.ChangedCollection)
      {
        data.Add(change.State + change.Data.ID, change.Data.ToString());
      }
      return data.Package();
    }

    public static Dictionary<string, T> UnPackedChangedData<T>(this string data)
    {
      var dictionary = UnPackedToDictionary(data);
      return dictionary.ToDictionary(change => change.Key, change => change.Value.Load<T>());
    }

    public static T LoadData<T>(this IDictionary<string, string> dictionary, string key)
      where T : new()
    {
      if (dictionary.ContainsKey(key))
        return dictionary[key].Load<T>();
      return default(T);
    }
  }
}