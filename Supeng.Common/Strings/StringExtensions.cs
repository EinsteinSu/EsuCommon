using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Supeng.Common.Strings
{
  public static class StringExtensions
  {
    public const char Split = '△';

    public static T Load<T>(this string data)
    {
      return JsonConvert.DeserializeObject<T>(data);
    }

    public static string SplitToString(this IList<string> collection, char c)
    {
      return collection.Aggregate(string.Empty, (current, s) => current + string.Format("{0}{1}", s, c)).TrimEnd(c);
    }

    public static List<string> GetStringCollection(this string str, char c)
    {
      return str.Split(c).ToList();
    }

    public static string Package(this IDictionary<string, string> dictionary)
    {
      var sb = new StringBuilder(dictionary.Count * 2);
      foreach (KeyValuePair<string, string> valuePair in dictionary)
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
  }
}
