using System;
using DevExpress.Xpf.LayoutControl;
using Supeng.Silverlight.Common.Interfaces;

namespace Supeng.Silverlight.ViewModel.Interfaces
{
  public interface IReturnChildWindow<T> : IDataLoad
  {
    Action<DataLayoutControlAutoGeneratingItemEventArgs> GeneratItemAction { get; set; }

    T CurrentData { get; set; }
  }
}