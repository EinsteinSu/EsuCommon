using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      EsuToolBarButtonCollection collection = new EsuToolBarButtonCollection(EsuButtonToolBarType.TextAndImage);
      EsuButtonBase button = new EsuButtonBase("Test", 0, DoAction){Description = "Test button"};
      collection.Add(button);
      collection.Add((EsuButtonBase)button.Clone());
      collection.Add((EsuButtonBase)button.Clone());
      collection.Add((EsuButtonBase)button.Clone());

      DataContext = collection;
    }

    private void DoAction()
    {
      MessageBox.Show("Button Click");
    }
  }

  public class TestData : EsuInfoBase
  {

  }
}
