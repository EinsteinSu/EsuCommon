using System.ComponentModel.DataAnnotations;
using Supeng.Common.IOs;

namespace Supeng.Common.Entities.BasesEntities
{
  public abstract class EsuConnectionBase : EsuInfoBase
  {
    private string dataSource;
    private string initialCatalog;
    private string password;
    private string userID;

    #region properties

    [Display(Name = "Data source")]
    public string DataSource
    {
      get { return dataSource; }
      set
      {
        if (value == dataSource) return;
        dataSource = value;
        NotifyOfPropertyChange(() => DataSource);
      }
    }

    [Display(Name = "Initial catalog")]
    public string InitialCatalog
    {
      get { return initialCatalog; }
      set
      {
        if (value == initialCatalog) return;
        initialCatalog = value;
        NotifyOfPropertyChange(() => InitialCatalog);
      }
    }

    [Display(Name = "User id")]
    public string UserID
    {
      get { return userID; }
      set
      {
        if (value == userID) return;
        userID = value;
        NotifyOfPropertyChange(() => UserID);
      }
    }

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password
    {
      get { return password; }
      set
      {
        if (value == password) return;
        password = value;
        NotifyOfPropertyChange(() => Password);
      }
    }

    #endregion

    [Display(AutoGenerateField = false)]
    protected virtual string SaveDirectory
    {
      get { return DirectoryHelper.DataDirectory; }
    }

    [Display(AutoGenerateField = false)]
    protected abstract string FileName { get; }

    [Display(AutoGenerateField = false)]
    public virtual string SaveFileName
    {
      get { return string.Format("{0}{1}", SaveDirectory, FileName); }
    }

    public virtual string ConnectionString
    {
      get
      {
        return string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
          DataSource, InitialCatalog, UserID, Password);
      }
    }

    public void SaveToXml()
    {
      SerializeToXml(SaveFileName);
    }

    public void SaveToText()
    {
      SerializeToText(SaveFileName);
    }
  }

  public class EsuConnection : EsuConnectionBase
  {
    [Display(AutoGenerateField = false)]
    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    protected override string FileName
    {
      get { return "Connection.xml"; }
    }

    public override string ConnectionString
    {
      get
      {
        return string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
          DataSource, InitialCatalog, UserID, Password);
      }
    }
  }
}