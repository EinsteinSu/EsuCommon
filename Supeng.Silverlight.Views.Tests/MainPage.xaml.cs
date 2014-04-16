using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Supeng.Silverlight.Common.Entities.BasesEntities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.ViewModel.WindowViewModels;

namespace Supeng.Silverlight.Views.Tests
{
  public partial class MainPage : UserControl
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var window = Views.ViewsHelper.ShowTreeSelectionWindow(new JzflSelectionViewModel());

    }
  }

  public sealed class JzflSelectionViewModel : TreeSelectionViewModel<Jzfl>
  {
    public override EsuInfoCollection<Jzfl> GetCollection()
    {
      return new JzflCollection();
    }

    public new Jzfl CurrentItem
    {
      get { return base.CurrentItem; }
      set
      {
        base.CurrentItem = value;
      }
    }

    public override string Title
    {
      get { return "选择建筑分类"; }
    }
  }

  public class Jzfl : TreeEntityBase
  {
    private string name;

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

    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }
  }

  public class JzflCollection : EsuInfoCollection<Jzfl>
  {
    private Jzfl currentJzfl;

    public JzflCollection()
    {
      Add(new Jzfl { ID = "1", PID = "0", Name = "民用" });
      Add(new Jzfl { ID = "3", PID = "1", Name = "高层" });
      Add(new Jzfl { ID = "5", PID = "3", Name = "公建" });
      Add(new Jzfl { ID = "10", PID = "5", Name = "Ⅰ类" });
      Add(new Jzfl { ID = "11", PID = "5", Name = "Ⅱ类" });
      Add(new Jzfl { ID = "12", PID = "5", Name = "地下室" });
      Add(new Jzfl { ID = "6", PID = "3", Name = "住宅" });
      Add(new Jzfl { ID = "13", PID = "6", Name = "Ⅰ类" });
      Add(new Jzfl { ID = "14", PID = "6", Name = "Ⅱ类" });
      Add(new Jzfl { ID = "15", PID = "6", Name = "地下室" });
      Add(new Jzfl { ID = "4", PID = "1", Name = "多层" });
      Add(new Jzfl { ID = "7", PID = "4", Name = "公建" });
      Add(new Jzfl { ID = "8", PID = "4", Name = "住宅" });
      Add(new Jzfl { ID = "9", PID = "4", Name = "地下室" });
      Add(new Jzfl { ID = "2", PID = "0", Name = "工建" });
      Add(new Jzfl { ID = "16", PID = "2", Name = "地下室" });
    }

    public Jzfl CurrentJzfl
    {
      get { return currentJzfl; }
      set
      {
        currentJzfl = value;
        OnPropertyChanged(new PropertyChangedEventArgs("CurrentJzfl"));
      }
    }

    public string GetCurrentJzfl()
    {
      string nodeString = string.Empty;
      for (int i = GetCurrentList().Count - 1; i >= 0; i--)
        nodeString += string.Format("{0}-", GetCurrentList()[i]);
      return nodeString.TrimEnd('-');
    }

    protected List<string> GetCurrentList()
    {
      var list = new List<string>();
      if (currentJzfl != null)
      {
        list.Add(currentJzfl.Name);
        while (true)
        {
          Jzfl jzfl = this.FirstOrDefault(f => f.ID == currentJzfl.PID);
          if (jzfl == null)
            break;
          list.Add(jzfl.Name);
        }
      }
      return list;
    }
  }
}
