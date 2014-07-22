using System.Windows;
using System.Windows.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for NormalSearchControlTest.xaml
  /// </summary>
  public partial class NormalSearchControlTest : UserControl
  {
    public NormalSearchControlTest()
    {
      InitializeComponent();

      DataContext = new SearchControlTestViewModel();
    }
  }

  public class SearchControlTestViewModel : SearchControlViewModel
  {
    private ISearchModel data;

    public SearchControlTestViewModel()
    {
      data = new TestSearchControlModel();
    }

    protected override string TemplateName
    {
      get { return "test"; }
    }

    public override FrameworkElement Content
    {
      get { return new TextBox(); }
    }

    public override ISearchModel Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    protected override void Search()
    {
      MessageBox.Show("Test");
    }
  }
}