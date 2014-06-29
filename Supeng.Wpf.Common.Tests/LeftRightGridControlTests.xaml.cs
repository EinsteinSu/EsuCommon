using System.Windows;
using System.Windows.Controls;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for LeftRightGridControlTests.xaml
  /// </summary>
  public partial class LeftRightGridControlTests : UserControl
  {
    public LeftRightGridControlTests()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      DialogWindowHelper.ShowDialogWindow(new LeftRightControlTest());
    }
  }

  internal class LeftRightControlTest : LeftRightWindowViewModelBase<TestData>
  {
    protected override EsuInfoCollection<TestData> InitalizeLeftCollection()
    {
      var collection = new EsuInfoCollection<TestData>();
      for (int i = 0; i < 10; i++)
      {
        collection.Add(new TestData {ID = i.ToString()});
      }
      return collection;
    }

    protected override EsuInfoCollection<TestData> InitalizeRightCollection()
    {
      return new EsuInfoCollection<TestData>();
    }
  }
}