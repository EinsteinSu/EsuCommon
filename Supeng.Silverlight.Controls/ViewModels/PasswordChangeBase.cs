using System.ComponentModel.DataAnnotations;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.Controls.ViewModels
{
  public abstract class PasswordChangeBase : EsuInfoBase
  {
    private string originalPassword;
    private string newPassword;
    private string newPasswordAgain;
    private readonly DelegateCommand updatePasswordCommand;

    protected PasswordChangeBase()
    {
      updatePasswordCommand = new DelegateCommand(UpdatePassword, () => true);
    }

    #region properties

    [DataType(DataType.Password)]
    [Display(Name = "请输入旧密码")]
    public string OriginalPassword
    {
      get { return originalPassword; }
      set
      {
        if (value == originalPassword) return;
        originalPassword = value;
        NotifyOfPropertyChange(() => OriginalPassword);
      }
    }

    [DataType(DataType.Password)]
    [Display(Name = "请输入新密码")]
    public string NewPassword
    {
      get { return newPassword; }
      set
      {
        if (value == newPassword) return;
        newPassword = value;
        NotifyOfPropertyChange(() => NewPassword);
      }
    }

    [DataType(DataType.Password)]
    [Display(Name = "请再次输入新密码")]
    public string NewPasswordAgain
    {
      get { return newPasswordAgain; }
      set
      {
        if (value == newPasswordAgain) return;
        newPasswordAgain = value;
        NotifyOfPropertyChange(() => NewPasswordAgain);
      }
    }
    #endregion

    [Display(AutoGenerateField = false)]
    public string ErrorMessage
    {
      get
      {
        string errMsg = string.Empty;
        if (string.IsNullOrEmpty(originalPassword) || string.IsNullOrEmpty(newPassword) ||
            string.IsNullOrEmpty(newPasswordAgain))
          errMsg += "密码不能为空！\n";
        if (newPassword != null && !newPassword.Equals(newPasswordAgain))
          errMsg = "两次输入的密码不一致！\n";
        return errMsg;
      }
    }

    [Display(AutoGenerateField = false)]
    public DelegateCommand UpdatePasswordCommand
    {
      get { return updatePasswordCommand; }
    }

    protected abstract void UpdatePassword();
  }
}
