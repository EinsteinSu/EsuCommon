namespace Supeng.Common.Entities.BasesEntities.DataEntities
{
  public class ContactInfo : EsuInfoBase
  {
    private string address;
    private string email;
    private string fax;
    private string mobile;
    private string phone;
    private string qq;
    private string user;
    private string weixin;

    #region properties

    /// <summary>
    ///   联系人
    /// </summary>
    public string User
    {
      get { return user; }
      set
      {
        if (value == user) return;
        user = value;
        NotifyOfPropertyChange(() => User);
      }
    }

    public string Phone
    {
      get { return phone; }
      set
      {
        if (value == phone) return;
        phone = value;
        NotifyOfPropertyChange(() => Phone);
      }
    }

    public string Fax
    {
      get { return fax; }
      set
      {
        if (value == fax) return;
        fax = value;
        NotifyOfPropertyChange(() => Fax);
      }
    }

    public string Mobile
    {
      get { return mobile; }
      set
      {
        if (value == mobile) return;
        mobile = value;
        NotifyOfPropertyChange(() => Mobile);
      }
    }

    public string Weixin
    {
      get { return weixin; }
      set
      {
        if (value == weixin) return;
        weixin = value;
        NotifyOfPropertyChange(() => Weixin);
      }
    }

    public string QQ
    {
      get { return qq; }
      set
      {
        if (value == qq) return;
        qq = value;
        NotifyOfPropertyChange(() => QQ);
      }
    }

    public string Email
    {
      get { return email; }
      set
      {
        if (value == email) return;
        email = value;
        NotifyOfPropertyChange(() => Email);
      }
    }

    public string Address
    {
      get { return address; }
      set
      {
        if (value == address) return;
        address = value;
        NotifyOfPropertyChange(() => Address);
      }
    }

    #endregion

    public string GetUserNameWithMobile()
    {
      return string.IsNullOrEmpty(mobile) ? user : string.Format("{0}({1})", user, mobile);
    }
  }
}