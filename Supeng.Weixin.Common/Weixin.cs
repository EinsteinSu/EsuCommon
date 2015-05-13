using System;
using Newtonsoft.Json;
using Supeng.Http.Common;
using Supeng.Weixin.Common.Message;
using Supeng.Weixin.Common.Properties;

namespace Supeng.Weixin.Common
{
  public class Weixin
  {
    private readonly string corpId;
    private readonly string secretId;
    private AccessToken accessToken;

    public Weixin(string corpId, string secretId)
    {
      this.corpId = corpId;
      this.secretId = secretId;
    }


    public AccessToken Token
    {
      get
      {
        if (accessToken == null)
        {
          string url = string.Format(Resources.ConnectUrl, corpId, secretId);
          using (var client = new EsuWebClient())
          {
            accessToken = client.GetData<AccessToken>(url);
          }
        }
        return accessToken;
      }
    }

    public string GetString(string url)
    {
      using (var client = new EsuWebClient())
      {
        return client.GetString(url);
      }
    }

    public T Get<T>(string url)
    {
      return JsonConvert.DeserializeObject<T>(GetString(url));
    }

    public string Post(string url, string data)
    {
      using (var client = new EsuWebClient())
      {
        return client.Post(url, data);
      }
    }

    public T Post<T>(string url, string data)
    {
      using (var client = new EsuWebClient())
      {
        return JsonConvert.DeserializeObject<T>(client.Post(url, data));
      }
    }
  }
}
