using System;

namespace Supeng.Common.Entities.BasesEntities
{
  public class ActionBase : EsuInfoBase
  {
    private int category;
    private string model;
    private string name;
    private string type;

    #region properties
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

    public string Type
    {
      get { return type; }
      set
      {
        if (value == type) return;
        type = value;
        NotifyOfPropertyChange(() => Type);
      }
    }

    public string Model
    {
      get { return model; }
      set
      {
        if (value == model) return;
        model = value;
        NotifyOfPropertyChange(() => Model);
      }
    }

    public int Category
    {
      get { return category; }
      set
      {
        if (value == category) return;
        category = value;
        NotifyOfPropertyChange(() => Category);
      }
    }

    public string ImageUrl
    {
      get
      {
        return string.Format("{0}\\Images\\Function\\{1}.png", Environment.CurrentDirectory, name);
      }
    }
    #endregion

  }
}