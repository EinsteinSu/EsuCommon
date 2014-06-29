using System.ComponentModel.DataAnnotations;
using System.Windows;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      var collection = new EsuToolBarButtonCollection(EsuButtonToolBarType.TextAndImage);
      var button = new EsuButtonBase("Test", 0, DoAction) {Description = "Test button"};
      collection.Add(button);
      collection.Add((EsuButtonBase) button.Clone());
      collection.Add((EsuButtonBase) button.Clone());
      collection.Add((EsuButtonBase) button.Clone());

      DataContext = collection;
    }

    private void DoAction()
    {
      MessageBox.Show("Button Click");
    }
  }

  public class TestData : EsuInfoBase
  {
    [Display(Name = @"ID")]
    public new string ID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [Display(Name = @"Description")]
    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }
  }
}