namespace Supeng.Silverlight.Common.Entities.BasesEntities.DataEntities
{
  public class UserInfoBase : EsuInfoBase
  {
    private string code;
    private ContactInfo contact;
    private string name;

    #region properties

    public string Code
    {
      get { return code; }
      set
      {
        if (value == code) return;
        code = value;
        NotifyOfPropertyChange(() => Code);
      }
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

    public ContactInfo Contact
    {
      get { return contact; }
      set
      {
        if (Equals(value, contact)) return;
        contact = value;
        NotifyOfPropertyChange(() => Contact);
      }
    }

    #endregion

    public virtual string DisplayName
    {
      get { return string.Format("{0}({1}-{2}", Name, Contact.Mobile, Contact.Address); }
    }
  }
}