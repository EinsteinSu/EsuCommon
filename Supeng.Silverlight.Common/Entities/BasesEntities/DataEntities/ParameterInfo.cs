using System.ComponentModel.DataAnnotations;

namespace Supeng.Silverlight.Common.Entities.BasesEntities.DataEntities
{
  public class ParameterInfo<T> : EsuInfoBase
  {
    private string category;
    private string name;
    private int orderID;
    private T value;

    #region properties
    [Display(Name = @"分组")]
    public string Category
    {
      get { return category; }
      set
      {
        if (value == category) return;
        category = value;
        NotifyOfPropertyChange(() => Category);
      }
    }

    [Display(Name = @"名称")]
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

    [Display(Name = @"值")]
    public T Value
    {
      get { return value; }
      set
      {
        if (Equals(value, this.value)) return;
        this.value = value;
        NotifyOfPropertyChange(() => Value);
      }
    }

    [Display(Name = @"排序编号")]
    public int OrderID
    {
      get { return orderID; }
      set
      {
        if (value == orderID) return;
        orderID = value;
        NotifyOfPropertyChange(() => OrderID);
      }
    }
    #endregion

  }
}