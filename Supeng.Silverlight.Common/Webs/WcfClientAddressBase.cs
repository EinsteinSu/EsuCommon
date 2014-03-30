using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Supeng.Silverlight.Common.Webs
{
  public class WcfClientAddressBase<T>
  {
    private readonly string url;

    public WcfClientAddressBase(string url)
    {
      this.url = url;
    }

    public virtual T GetService()
    {
      CustomBinding binding;
      EndpointAddress endPoint;
      GetDefaultBinding(out binding, out endPoint);
      var client = (T) Activator.CreateInstance(typeof (T), binding, endPoint);
      return client;
    }

    protected virtual void GetDefaultBinding(out CustomBinding binding, out EndpointAddress endPoint)
    {
      var basicHttpBinding = new BasicHttpBinding
      {
        MaxBufferSize = 2147483647,
        MaxReceivedMessageSize = 2147483647
      };
      binding = new CustomBinding(basicHttpBinding);
      endPoint = new EndpointAddress(url);
    }
  }
}