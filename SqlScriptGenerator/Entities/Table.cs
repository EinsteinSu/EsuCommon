using System.ComponentModel.DataAnnotations;
using System.Data;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace SqlScriptGenerator.Entities
{
  internal class Table : EsuInfoBase, IDataCreator<Table>
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

    [Display(AutoGenerateField = false)]
    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    public Table CreateData(IDataReader reader)
    {
      return new Table
      {
        ID = reader["object_id"].ToString(),
        Name = reader["name"].ToString()
      };
    }
  }

  internal class TableCollection : EsuInfoCollection<Table>
  {
    public TableCollection(DataStorageBase storage)
    {
      const string sql = "Select * from sys.tables order by name";
      foreach (Table table in storage.ReadToCollection(sql, new Table()))
      {
        Add(table);
      }
    }
  }
}