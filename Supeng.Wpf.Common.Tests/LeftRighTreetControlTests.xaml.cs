using System.Windows;
using System.Windows.Controls;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for LeftRightControlTests.xaml
  /// </summary>
  public partial class LeftRightTreeControlTests : UserControl
  {
    public LeftRightTreeControlTests()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      DialogWindowHelper.ShowDialogWindow(new TreeLeftRightControlViewModel());
    }
  }

  public class TreeLeftRightControlViewModel : TreeLeftRightWindowViewModelBase<TestTreeData>
  {
    protected override EsuInfoCollection<TestTreeData> InitalizeRightCollection()
    {
      return base.InitalizeRightCollection();
    }

    protected override EsuInfoCollection<TestTreeData> InitalizeLeftCollection()
    {
      var collection = new EsuInfoCollection<TestTreeData>();
      collection.Add(new TestTreeData("1", "0", "Parent1"));

      for (int i = 2; i < 10; i++)
      {
        collection.Add(new TestTreeData(i.ToString(), "1", "child" + i));
      }
      //collection.Add(new TestTreeData("10", "0", "Parent1"));

      //for (int i = 10; i < 20; i++)
      //{
      //  collection.Add(new TestTreeData(i.ToString(), "1", "child2"));
      //}
      return collection;
    }

    protected override void RightClick()
    {
      base.RightClick();
    }
  }

  public class TestTreeData : TreeEntityBase
  {
    private string name;

    public TestTreeData(string id, string pid, string name)
    {
      ID = id;
      PID = pid;
      this.name = name;
    }

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

  }
}