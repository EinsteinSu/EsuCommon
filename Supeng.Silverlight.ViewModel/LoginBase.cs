using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.ViewModel
{
  public abstract class LoginBase : EsuInfoBase
  {
    private readonly DelegateCommand loginCommand;
    private string password;
    private bool rememberPassword;
    private bool rememberUserName;
    private string userName;

    protected LoginBase()
    {
      loginCommand = new DelegateCommand(LoginClick, () => true);
    }

    #region properties

    [Display(Name = @"用户名", Order = 0)]
    public string UserName
    {
      get { return userName; }
      set
      {
        if (value == userName) return;
        userName = value;
        NotifyOfPropertyChange(() => UserName);
      }
    }

    [DataType(DataType.Password)]
    [Display(Name = @"密码", Order = 1)]
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

    [Display(Name = @"记住用户名", GroupName = @"<remember>", Order = 1)]
    public bool RememberUserName
    {
      get { return rememberUserName; }
      set
      {
        if (value.Equals(rememberUserName)) return;
        rememberUserName = value;
        NotifyOfPropertyChange(() => RememberUserName);
      }
    }

    [Display(Name = @"记住密码", GroupName = @"<remember>", Order = 1)]
    public bool RememberPassword
    {
      get { return rememberPassword; }
      set
      {
        if (value.Equals(rememberPassword)) return;
        rememberPassword = value;
        if (value)
          RememberUserName = true;
        NotifyOfPropertyChange(() => RememberPassword);
      }
    }

    #endregion

    #region commands

    [Display(AutoGenerateField = false)]
    public DelegateCommand LoginCommand
    {
      get { return loginCommand; }
    }

    #endregion

    protected virtual string CheckLoginError()
    {
      string errMsg = string.Empty;
      if (string.IsNullOrEmpty(userName))
        errMsg += "用户名不能为空！" + Environment.NewLine;
      if (string.IsNullOrEmpty(password))
        errMsg += "密码不能为空!" + Environment.NewLine;
      return errMsg;
    }

    protected virtual void LoginClick()
    {
      string errMsg = CheckLoginError();
      if (string.IsNullOrEmpty(errMsg))
      {
        MessageBox.Show(errMsg);
        return;
      }
      Login();
    }

    protected abstract bool Login();
  }
}