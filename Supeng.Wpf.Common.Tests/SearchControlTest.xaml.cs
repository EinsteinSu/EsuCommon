using System.ComponentModel.DataAnnotations;
using System.Windows;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for SearchControlTest.xaml
  /// </summary>
  public partial class SearchControlTest
  {
    public SearchControlTest()
    {
      InitializeComponent();

      DataContext = new TestSearchControlViewModel();
    }
  }

  public class TestSearchControlViewModel : SearchControlWithDataLayoutViewModel
  {
    private ISearchModel searchModel;

    public TestSearchControlViewModel()
    {
      searchModel = new TestSearchControlModel();
    }

    public override ISearchModel SearchModel
    {
      get { return searchModel; }
      set { searchModel = value; }
    }

    protected override void Search()
    {
      MessageBox.Show(searchModel.Search());
    }
  }

  public class TestSearchControlModel : ISearchModel
  {
    [Display(Name = @"Condition1")]
    public string Condition1 { get; set; }

    [Display(Name = @"Condition2")]
    public string Condition2 { get; set; }

    public string Search()
    {
      return "Test Search";
    }

    public void Clear()
    {
      MessageBox.Show("Clear");
    }
  }
}