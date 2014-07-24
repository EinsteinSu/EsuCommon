namespace Supeng.Silverlight.Controls.ViewModels
{
  public abstract class AboutViewModelBase
  {
    public virtual string ImageUrl
    {
      get { return "/Images/Logo.png"; }
    }

    public abstract string ProductName { get; }

    public abstract string CustomerCompany { get; }

    public abstract string Version { get; }

    public virtual string Company
    {
      get { return "昆明简捷科技有限公司"; }
    }

    public virtual string Tel
    {
      get { return "13825634085"; }
    }

    public virtual string QQ
    {
      get { return "76507593"; }
    }

    public virtual string Email
    {
      get { return "einstein_supeng@hotmail.com"; }
    }
  }
}
