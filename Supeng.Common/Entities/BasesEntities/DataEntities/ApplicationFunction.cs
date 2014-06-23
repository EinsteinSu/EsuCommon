using System;
using System.Data;
using Supeng.Common.DataOperations;

namespace Supeng.Common.Entities.BasesEntities.DataEntities
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
      get
      {
        return string.Format("{0}\\Images\\Functionalities\\{1}.png", Environment.CurrentDirectory, name);
      }
    }
  }

  public class ApplicationFunctionCreator : IDataCreator<ApplicationFunction>
  {
    public ApplicationFunction CreateData(IDataReader reader)
    {
      return new ApplicationFunction
      {
        ID = reader["ID"].ToString(),
        Name = reader["Name"].ToString(),
        Model = reader["Model"].ToString(),
        Type = reader["Type"].ToString(),
        Category = int.Parse(reader["Category"].ToString())
      };
    }
  }
}