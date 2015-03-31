using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Controls;
using Supeng.Silverlight.Common.Interfaces;

namespace Supeng.Silverlight.Common.Entities
{
  public class EsuCrudInfoBase<T> : EsuInfoBase
  {
    private EsuCommand insertCommand;
    private EsuCommandWithParameter<T> updateCommand;
    private EsuCommandWithParameter<T> deleteCommand;
    private EsuCommandWithParameter<T> todoCommand;
    private EsuCommandWithParameter<T> todoCommand1;
    private EsuCommandWithParameter<T> todoCommand2;
    private EsuCommandWithParameter<T> todoCommand3;
    public EsuCrudInfoBase() { }

    public EsuCrudInfoBase(Action insertAction, Action<T> updateAction,
      Action<T> deleteAction, Func<bool> insertCanExecute = null, Func<bool> updateCanExecute = null,
      Func<bool> deleteCanExecute = null)
    {
      SetCommands(insertAction, updateAction, deleteAction, insertCanExecute, updateCanExecute, deleteCanExecute);
    }

    [JsonIgnore]
    public EsuCommand InsertCommand
    {
      get { return insertCommand; }
      set
      {
        if (Equals(value, insertCommand)) return;
        insertCommand = value;
        NotifyOfPropertyChange(() => InsertCommand);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> UpdateCommand
    {
      get { return updateCommand; }
      set
      {
        if (Equals(value, updateCommand)) return;
        updateCommand = value;
        NotifyOfPropertyChange(() => UpdateCommand);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> DeleteCommand
    {
      get { return deleteCommand; }
      set
      {
        if (Equals(value, deleteCommand)) return;
        deleteCommand = value;
        NotifyOfPropertyChange(() => DeleteCommand);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> TodoCommand
    {
      get { return todoCommand; }
      set
      {
        if (Equals(value, todoCommand)) return;
        todoCommand = value;
        NotifyOfPropertyChange(() => TodoCommand);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> TodoCommand1
    {
      get { return todoCommand1; }
      set
      {
        if (Equals(value, todoCommand1)) return;
        todoCommand1 = value;
        NotifyOfPropertyChange(() => TodoCommand1);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> TodoCommand2
    {
      get { return todoCommand2; }
      set
      {
        if (Equals(value, todoCommand2)) return;
        todoCommand2 = value;
        NotifyOfPropertyChange(() => TodoCommand2);
      }
    }

    [JsonIgnore]
    public EsuCommandWithParameter<T> TodoCommand3
    {
      get { return todoCommand3; }
      set
      {
        if (Equals(value, todoCommand3)) return;
        todoCommand3 = value;
        NotifyOfPropertyChange(() => TodoCommand3);
      }
    }

    public void SetCommands(Action insertAction, Action<T> updateAction,
      Action<T> deleteAction, Func<bool> insertCanExecute = null, Func<bool> updateCanExecute = null, Func<bool> deleteCanExecute = null)
    {
      InsertCommand = new EsuCommand(insertAction, insertCanExecute);
      UpdateCommand = new EsuCommandWithParameter<T>(updateAction, updateCanExecute);
      DeleteCommand = new EsuCommandWithParameter<T>(deleteAction, deleteCanExecute);
    }

    public void SetCommands(ICrud<T> crud)
    {
      SetCommands(crud.Create, crud.Update, crud.Delete);
    }

    public void SetTodoCommand(Action<T> todoAction, Func<bool> todoCanExecute = null)
    {
      TodoCommand = new EsuCommandWithParameter<T>(todoAction, todoCanExecute);
    }

    public void SetTodoCommands(IList<Action<T>> todoActions,
      IList<Func<bool>> todoCanExecutes = null)
    {
      TodoCommand = new EsuCommandWithParameter<T>(todoActions[0], todoCanExecutes == null ? null : todoCanExecutes[0]);
      TodoCommand1 = new EsuCommandWithParameter<T>(todoActions[1], todoCanExecutes == null ? null : todoCanExecutes[1]);
      TodoCommand2 = new EsuCommandWithParameter<T>(todoActions[2], todoCanExecutes == null ? null : todoCanExecutes[2]);
      TodoCommand3 = new EsuCommandWithParameter<T>(todoActions[3], todoCanExecutes == null ? null : todoCanExecutes[3]);
    }
  }
}
