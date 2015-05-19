#region

using System.Linq;
using System.Windows;
using Supeng.Common.Controls;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

#endregion

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for DialogWindowsTest.xaml
  /// </summary>
  public partial class DialogWindowsTest
  {
    public DialogWindowsTest()
    {
      InitializeComponent();
      DataContext = new DialogWindowsTestViewModel();
    }
  }

  public class DialogWindowsTestViewModel : EsuInfoBase
  {
    public DialogWindowsTestViewModel()
    {
      FindItemWindowTestCommand = new EsuCommand(() =>
      {
        var viewModel = new TestFindItemWindow();
        if (viewModel.ShowDialogWindowWithReturn())
        {
          MessageBox.Show(viewModel.Collection.CurrentItem.Description);
        }
      });
    }

    public EsuCommand FindItemWindowTestCommand { get; set; }
  }

  public class TestFindItemWindow : FindItemWindowViewModel<TestData>
  {
    public override void Load()
    {
      base.Load();
      Collection = new EsuInfoCollection<TestData>
      {
        new TestData("1", "User1"),
        new TestData("2", "User2"),
        new TestData("3", "User3")
      };
    }

    protected override void Search()
    {
      if (string.IsNullOrEmpty(SearchText))
        Load();
      var searchCollection = new EsuInfoCollection<TestData>();
      foreach (var data in Collection.Where(w => w.Description.Contains(SearchText)))
      {
        searchCollection.Add(data);
      }
      Collection = searchCollection;
    }
  }
}