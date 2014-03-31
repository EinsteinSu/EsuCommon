using System;

namespace Supeng.Silverlight.Common.Interfaces.Controls
{
  public interface IShowProgress
  {
    Action<string> StartShowing { get; set; }

    Action StopShowing { get; set; }
  }
}