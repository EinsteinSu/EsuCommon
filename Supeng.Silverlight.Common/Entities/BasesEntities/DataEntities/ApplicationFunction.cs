namespace Supeng.Silverlight.Common.Entities.BasesEntities.DataEntities
{
  public class ApplicationFunction : EsuInfoBase
  {
    private int category;
    private string model;
    private string name;
    private string type;

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
      get { return string.Format("\\Images\\Function\\{0}.png", name); }
    }
  }
}