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
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for EsuInfoCollectionTest.xaml
  /// </summary>
  public partial class EsuInfoCollectionTest : UserControl
  {
    public EsuInfoCollectionTest()
    {
      InitializeComponent();

      DataContext = new TestModel();
    }
  }

  public class TestModel : EsuInfoBase
  {
    private EsuInfoCollection<TestData> collection;
    private string text;

    public TestModel()
    {
      collection = new EsuInfoCollection<TestData>();
      for (int i = 0; i < 10; i++)
      {
        collection.Add(new TestData { ID = i.ToString(), Description = "Description" });
      }
      collection.CurrentItemChanged = (s) =>
      {
        text = s.ID;
        NotifyOfPropertyChange(() => Text);
      };
    }

    public string Text
    {
      get { return text; }
    }

    public EsuInfoCollection<TestData> Collection
    {
      get { return collection; }
      set
      {
        if (Equals(value, collection)) return;
        collection = value;
        NotifyOfPropertyChange(() => Collection);
      }
    }
  }
}
