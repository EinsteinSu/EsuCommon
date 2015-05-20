using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class PreviewWindowBase : DialogWindowBase
  {
    public override FrameworkElement Content
    {
      get { return new PreviewEntityControl(); }
    }

    public override Visibility BottomVisibility
    {
      get { return Visibility.Collapsed; }
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}
