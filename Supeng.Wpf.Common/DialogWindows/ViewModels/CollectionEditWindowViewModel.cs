using System.Windows;
using Supeng.Common.Entities;
using Supeng.Data;
using Supeng.Wpf.Common.Controls.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class CollectionEditWindowViewModel<T> : DialogWindowBase where T : EsuInfoBase, new()
  {
    private EsuDataEditCollection<T> data;

    public EsuDataEditCollection<T> Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    public override FrameworkElement Content
    {
      get { return new CollectionEditGrid(); }
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}