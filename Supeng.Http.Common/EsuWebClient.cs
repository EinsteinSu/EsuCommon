using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Supeng.Http.Common
{
  public class EsuWebClient : IDisposable
  {
    private readonly WebClient client;
    public EsuWebClient()
    {
      client = new WebClient();
      client.Encoding = Encoding.UTF8;
    }

    public string GetString(string url)
    {
      return Encoding.UTF8.GetString(client.DownloadData(url));
    }

    public T GetData<T>(string url)
    {
      return JsonConvert.DeserializeObject<T>(GetString(url));
    }

    public string Post(string url, string data)
    {
      return client.UploadString(url, data);
    }

    public void Dispose()
    {
      client.Dispose();
    }
  }
}
