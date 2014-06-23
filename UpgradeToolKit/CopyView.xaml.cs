using System;
using System.Threading.Tasks;

namespace UpgradeToolKit
{
  /// <summary>
  ///   Interaction logic for CopyView.xaml
  /// </summary>
  public partial class CopyView
  {
    private readonly CopyViewModel _view;

    public CopyView()
    {
      InitializeComponent();
      _view = new CopyViewModel();
      DataContext = _view;

      Loaded += (s, e) => Task.Factory.StartNew(() =>
      {
        _view.StartCopy();
        Environment.Exit(-1);
      });
    }
  }
}