using System;

namespace Supeng.Common.Exceptions
{
  public interface IExceptionHandle
  {
    void Handle(Exception ex);
  }
}