using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;
using SqlScriptGenerator.Entities;
using Supeng.Common.Entities;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.IOs;
using Supeng.Data.Sql;
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace SqlScriptGenerator.ViewModels
{
  internal class ScriptGeneratorViewModel : EsuInfoBase
  {
    private readonly DelegateCommand entitiesCreator;
    private readonly DelegateCommand generateCreator;
    private readonly DelegateCommand generateScript;
    private readonly DelegateCommand readCommand;
    private ColumnCollection columnCollection;
    private EsuConnection connection;
    private Table currentTable;
    private string result;
    private TableCollection tableCollection;

    public ScriptGeneratorViewModel()
    {
      readCommand = new DelegateCommand(Read, () => true);
      generateCreator = new DelegateCommand(Creator, () => true);
      generateScript = new DelegateCommand(Script, () => true);
      entitiesCreator = new DelegateCommand(Entities, () => true);
    }

    #region properties

    public EsuConnection Connection
    {
      get { return connection; }
      set
      {
        if (Equals(value, connection)) return;
        connection = value;
        NotifyOfPropertyChange(() => Connection);
      }
    }

    public TableCollection TableCollection
    {
      get { return tableCollection; }
    }

    public Table CurrentTable
    {
      get { return currentTable; }
      set
      {
        if (Equals(value, currentTable)) return;
        currentTable = value;
        columnCollection = new ColumnCollection(value.Name,
          new SqlDataStorage(connection.ConnectionString, TaskScheduler.Current));
        NotifyOfPropertyChange(() => ColumnCollection);
        NotifyOfPropertyChange(() => CurrentTable);
      }
    }

    public ColumnCollection ColumnCollection
    {
      get { return columnCollection; }
    }

    public string Result
    {
      get { return result; }
      set
      {
        if (value == result) return;
        result = value;
        NotifyOfPropertyChange(() => Result);
      }
    }

    #endregion

    #region commands

    public DelegateCommand ReadCommand
    {
      get { return readCommand; }
    }

    public DelegateCommand GenerateScript
    {
      get { return generateScript; }
    }

    public DelegateCommand GenerateCreator
    {
      get { return generateCreator; }
    }

    public DelegateCommand EntitiesCreator
    {
      get { return entitiesCreator; }
    }

    protected virtual void Read()
    {
      var viewModel = new ConnectionEditViewModel(connection);
      Connection = DialogWindowHelper.GetEntityResult(viewModel);
      if (connection != null)
      {
        tableCollection = new TableCollection(new SqlDataStorage(connection.ConnectionString, TaskScheduler.Current));
        NotifyOfPropertyChange(() => tableCollection);
      }
    }

    protected virtual void Creator()
    {
      if (columnCollection != null)
        Result = columnCollection.GetCreatorString();
    }

    protected virtual void Script()
    {
      var sb = new StringBuilder();
      if (columnCollection != null)
      {
        sb.AppendLine(columnCollection.GetInsertSqlScript());
        sb.AppendLine(columnCollection.GetUpdateSql());
        sb.AppendLine(columnCollection.GetDeleteSql());
        Result = sb.ToString();
      }
    }

    protected virtual void Entities()
    {
      if (columnCollection != null)
        Result = columnCollection.GetEntities();
    }

    #endregion
  }

  internal class ConnectionEditViewModel : EntityEditViewModelBase<EsuConnection>
  {
    public ConnectionEditViewModel(EsuConnection data)
      : base(data)
    {
      if (Data == null)
      {
        Data = !File.Exists(string.Format("{0}Connection.xml", DirectoryHelper.DataDirectory))
          ? new EsuConnection()
          : "Connection.xml".LoadDataFromXmlInDataPath<EsuConnection>();
      }
    }

    protected override string DataCheck()
    {
      Data.SaveToXml();
      return string.Empty;
    }
  }
}