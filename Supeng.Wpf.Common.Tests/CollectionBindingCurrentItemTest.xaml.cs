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
using System.Windows.Shapes;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for CollectionBindingCurrentItemTest.xaml
  /// </summary>
  public partial class CollectionBindingCurrentItemTest : Window
  {
    public CollectionBindingCurrentItemTest()
    {
      InitializeComponent();

      DataContext = new CurrentItemTestViewModel();
    }
  }

  public class CurrentItemTestViewModel : EsuInfoBase
  {
    private EsuInfoCollection<EsuInfoBase> data;

    public EsuInfoCollection<EsuInfoBase> Data
    {
      get
      {
        if (data == null)
        {
          data = new EsuInfoCollection<EsuInfoBase>
        {
          new EsuInfoBase {ID = "doing",Description = "技术审查"},
          new EsuInfoBase {ID = "done",Description = "报告完成"},
          new EsuInfoBase {ID = "unassigned",Description = "未分配"},
        };
          data.CurrentItemChanged += (current) =>
          {
            MessageBox.Show(current.ID);
          };

        }
        return data;
      }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }
  }

  public class ItemData : EsuInfoBase
  {
    public override string ToString()
    {
      return Description;
    }
  }
}
