using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Supeng.Common.Controls;
using Supeng.Common.Interfaces;

namespace Supeng.Common.Entities
{
  public class EsuCrudInfoBase : EsuInfoBase
  {
    public EsuCrudInfoBase() { }

    public EsuCrudInfoBase(Action insertAction, Action updateAction,
      Action deleteAction, Func<bool> insertCanExecute = null, Func<bool> updateCanExecute = null,
      Func<bool> deleteCanExecute = null)
    {
      SetCommands(insertAction, updateAction, deleteAction, insertCanExecute, updateCanExecute, deleteCanExecute);
    }
    [JsonIgnore]
    public EsuCommand InsertCommand { get; set; }

    [JsonIgnore]
    public EsuCommand UpdateCommand { get; set; }

    [JsonIgnore]
    public EsuCommand DeleteCommand { get; set; }

    [JsonIgnore]
    public EsuCommand TodoCommand { get; set; }

    [JsonIgnore]
    public EsuCommand TodoCommand1 { get; set; }

    [JsonIgnore]
    public EsuCommand TodoCommand2 { get; set; }

    [JsonIgnore]
    public EsuCommand TodoCommand3 { get; set; }
    public void SetCommands(Action insertAction, Action updateAction,
      Action deleteAction, Func<bool> insertCanExecute = null, Func<bool> updateCanExecute = null, Func<bool> deleteCanExecute = null)
    {
      InsertCommand = new EsuCommand(insertAction, insertCanExecute);
      UpdateCommand = new EsuCommand(updateAction, updateCanExecute);
      DeleteCommand = new EsuCommand(deleteAction, deleteCanExecute);
    }

    public void SetCommands(ICrud crud)
    {
      SetCommands(crud.Create, crud.Update, crud.Delete);
    }

    public void SetTodoCommand(Action todoAction, Func<bool> todoCanExecute = null)
    {
      TodoCommand = new EsuCommand(todoAction, todoCanExecute);
    }

    public void SetTodoCommands(IList<Action> todoActions,
      IList<Func<bool>> todoCanExecutes = null)
    {
      TodoCommand = new EsuCommand(todoActions[0], todoCanExecutes == null ? null : todoCanExecutes[0]);
      TodoCommand1 = new EsuCommand(todoActions[1], todoCanExecutes == null ? null : todoCanExecutes[1]);
      TodoCommand2 = new EsuCommand(todoActions[2], todoCanExecutes == null ? null : todoCanExecutes[2]);
      TodoCommand3 = new EsuCommand(todoActions[3], todoCanExecutes == null ? null : todoCanExecutes[3]);
    }
  }
}
