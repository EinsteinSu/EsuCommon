using Newtonsoft.Json;

namespace Supeng.Silverlight.Common.Strings
{
  public static class StringExtensions
  {
    public static T Load<T>(this string data)
    {
      return JsonConvert.DeserializeObject<T>(data);
    }
  }
}