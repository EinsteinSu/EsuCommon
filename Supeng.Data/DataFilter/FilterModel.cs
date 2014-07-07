using Supeng.Common.Entities;

namespace Supeng.Data.DataFilter
{
  public class FilterModel : EsuInfoBase
  {
    private string name;
    private string filter;

    public FilterModel(string name, string columnName, string data, FilterType type = FilterType.String)
    {
      this.name = name;
      if (type == FilterType.String)
        this.SetStringFilter(columnName, data);
      else
        this.SetNumberFilter(columnName, data);
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

    public string Filter
    {
      get { return filter; }
      set
      {
        if (value == filter) return;
        filter = value;
        NotifyOfPropertyChange(() => Filter);
      }
    }
  }

  public enum FilterType
  {
    String, Numeric
  }

  public static class FilterModelExtensions
  {
    public static void SetStringFilter(this FilterModel model, string columnName, string data)
    {
      model.Filter = string.Format("{0} like '%{1}%'", columnName, data);
    }

    public static void SetNumberFilter(this FilterModel model, string columnName, string data)
    {
      model.Filter = string.Format("{0} = {1}", columnName, data);
    }
  }
}