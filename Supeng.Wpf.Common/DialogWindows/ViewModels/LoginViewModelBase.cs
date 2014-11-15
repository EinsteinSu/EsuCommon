using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows;
using DevExpress.Xpf.Bars;
using Newtonsoft.Json;
using Supeng.Common.IOs;
using Supeng.Common.Strings;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class LoginViewModelBase : DialogWindowBase
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand loginCommand;
    private readonly DelegateCommand updateCommand;
    private string password;
    private bool rememberPassword;
    private bool rememberUserName;
    private string userName;

    protected string TemplateFileName = DirectoryHelper.DataDirectory + "Login.txt";
    private bool result;

    protected LoginViewModelBase()
    {
      loginCommand = new DelegateCommand(LoginClick, () => true);
      cancelCommand = new DelegateCommand(CancelClick, () => true);
      updateCommand = new DelegateCommand(UpdateClick, () => true);
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

    [Display(AutoGenerateField = false)]
    new public bool Result
    {
      get { return result; }
      set
      {
        if (value.Equals(result)) return;
        result = value;
        NotifyOfPropertyChange(() => Result);
      }
    }

    #endregion

    #region commands

    [Display(AutoGenerateField = false)]
    public DelegateCommand LoginCommand
    {
      get { return loginCommand; }
    }

    [Display(AutoGenerateField = false)]
    new public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    [Display(AutoGenerateField = false)]
    public DelegateCommand UpdateCommand
    {
      get { return updateCommand; }
    }

    #endregion

    #region methods

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public Action LoginDone { get; set; }

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
      if (!string.IsNullOrEmpty(errMsg))
      {
        MessageBox.Show(errMsg);
        return;
      }
      if (Login())
      {
        Result = true;
        if (Window != null)
          Window.Close();
        if (LoginDone != null)
          LoginDone();
      }
    }

    new protected virtual void CancelClick()
    {
      Environment.Exit(0);
    }

    protected virtual void UpdateClick()
    {
      string fileName = string.Format("{0}\\Update.exe", Environment.CurrentDirectory);
      if (File.Exists(fileName))
      {
        Process.Start(fileName);
        Environment.Exit(0);
      }
      else
      {
        throw new FileNotFoundException("未能找到升级组件，请联系系统管理员 ...");
      }
    }

    #endregion

    public virtual void SaveDataToTextFiles()
    {
      Password = Password.Encrypt();
      SerializeToText(TemplateFileName);
    }

    protected abstract bool Login();
  }
}