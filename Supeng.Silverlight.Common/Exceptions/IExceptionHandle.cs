using System;

namespace Supeng.Silverlight.Common.Exceptions
{
  public interface IExceptionHandle
  {
    void Handle(Exception ex);
  }
}
