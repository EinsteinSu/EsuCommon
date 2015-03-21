using System;
using System.Windows.Input;

namespace Supeng.Common.Controls
{
  public class EsuCommand : ICommand
  {
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    public EsuCommand(Action execute, Func<bool> canExecute = null)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute();
    }

    public void Execute(object parameter)
    {
      execute();
    }

    public event EventHandler CanExecuteChanged;
  }

  public class EsuCommandWithParameter<T> : ICommand
  {
    private readonly Action<T> execute;
    private readonly Func<bool> canExecute;

    public EsuCommandWithParameter(Action<T> execute, Func<bool> canExecute = null)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute();
    }

    public void Execute(object parameter)
    {
      var data = (T)parameter;
      execute(data);
    }

    public event EventHandler CanExecuteChanged;
  }
}
