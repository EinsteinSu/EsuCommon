using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.LayoutControl;
using Supeng.Silverlight.Common;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ControlEntities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.ViewModel.Interfaces;

namespace Supeng.Silverlight.ViewModel.Controls.ManagementControls
{
  public abstract class DataEditWithWindowViewModel<T> : ManagementWithToolbarViewModelBase, IDataLoad where T : EsuInfoBase, new()
  {
    private readonly DelegateCommand addCommand;
    private readonly DelegateCommand deleteCommand;
    private readonly DelegateCommand modifyCommand;
    private readonly DelegateCommand refreshCommand;
    private readonly ChildWindow returnWindow;
    private readonly bool useSaveButton; // is it use the save button if it not use then save data when user do any operation.
    private readonly DelegateCommand saveCommand;
    private T currentItem;
    private EsuInfoCollection<T> dataCollection;
    private Action<DataLayoutControlAutoGeneratingItemEventArgs> generateItem;

    protected DataEditWithWindowViewModel(ChildWindow returnWindow, bool useSaveButton = true)
    {
      this.returnWindow = returnWindow;
      this.useSaveButton = useSaveButton;
      refreshCommand = new DelegateCommand(Load, () => true);
      addCommand = new DelegateCommand(Add, () => true);
      modifyCommand = new DelegateCommand(Modify, () => true);
      deleteCommand = new DelegateCommand(Remove, () => true);
      saveCommand = new DelegateCommand(SaveData, () => true);
      InitalizeButtons();
      generateItem = GenerateEditor;
    }

    public Action<DataLayoutControlAutoGeneratingItemEventArgs> GenerateItem
    {
      get { return generateItem; }
      set
      {
        if (Equals(value, generateItem)) return;
        generateItem = value;
        NotifyOfPropertyChange(() => GenerateItem);
      }
    }

    public abstract string EntityName { get; }

    protected virtual int WindowWidth
    {
      get { return 300; }
    }

    protected virtual int WindowHeight
    {
      get { return 400; }
    }

    protected abstract void GenerateEditor(DataLayoutControlAutoGeneratingItemEventArgs e);

    public void Load()
    {
      if (dataCollection != null && dataCollection.HasChanged && useSaveButton)
      {
        MessageBoxResult result = RefreshBoxResult;
        if (result == MessageBoxResult.Yes)
          SaveData();
        else if (result == MessageBoxResult.Cancel)
          return;
      }
      if (StartShowing != null)
        StartShowing("加载数据");
      Task.Factory.StartNew(() =>
      {
        LoadData();
        dataCollection.AcceptChanges();
        if (StopShowing != null)
          StopShowing();
      });
    }

    protected override void InitalizeButtons()
    {
      var refreshButton = new EsuToolbarButton
      {
        Title = "刷新",
        ToolTip = "刷新数据",
        Image = GetImageSourceByName("Refresh"),
        Command = RefreshCommand
      };
      ButtonCollection.Add(refreshButton);

      var addButton = new EsuToolbarButton(10)
      {
        Title = "增加",
        ToolTip = "新增数据",
        Image = GetImageSourceByName("Add"),
        Command = AddCommand
      };
      ButtonCollection.Add(addButton);

      var modifyButton = new EsuToolbarButton
      {
        Title = "修改",
        ToolTip = "修改数据",
        Image = GetImageSourceByName("Modify"),
        Command = ModifyCommand
      };
      ButtonCollection.Add(modifyButton);

      var deleteButton = new EsuToolbarButton
      {
        Title = "删除",
        ToolTip = "删除选中数据",
        Image = GetImageSourceByName("Delete"),
        Command = DeleteCommand
      };
      ButtonCollection.Add(deleteButton);

      if (useSaveButton)
      {
        var saveButton = new EsuToolbarButton(10)
        {
          Title = "保存",
          ToolTip = "保存数据",
          Image = GetImageSourceByName("Save"),
          Command = SaveCommand
        };
        ButtonCollection.Add(saveButton);
      }

    }


    #region data operations
    protected virtual IReturnChildWindow<T> ProcessReturnWindow(T data)
    {
      returnWindow.Width = WindowWidth;
      returnWindow.Height = WindowHeight;
      var window = returnWindow as IReturnChildWindow<T>;
      if (window != null)
      {
        window.CurrentData = data;
        window.GeneratItemAction = generateItem;
        window.Load();
        return window;
      }
      return null;
    }

    protected virtual void Add()
    {
      var data = InitailizeDefaultData<T>();
      CurrentItem = data;
      ProcessReturnWindow(data);
      returnWindow.Show();
      returnWindow.Closed += (sender, args) =>
      {
        if (returnWindow.DialogResult != null && returnWindow.DialogResult.Value)
        {
          if (data != null)
          {
            DataCollection.Add(data);
            if (!useSaveButton)
              Save();
          }
        }
      };
    }

    protected virtual void Modify()
    {
      if (currentItem != null)
      {
        string original = currentItem.ToString();
        IReturnChildWindow<T> window = ProcessReturnWindow(currentItem);
        returnWindow.Show();
        returnWindow.Closed += (sender, args) =>
        {
          if (returnWindow.DialogResult != null && returnWindow.DialogResult.Value)
          {
            CurrentItem = DataCollection[DataCollection.IndexOf(CurrentItem)] = window.CurrentData;
            if (!useSaveButton)
              Save();
          }
          else
            CurrentItem = original.Load<T>();
        };
        NotifyOfPropertyChange(() => DataCollection);
      }
    }

    protected virtual void Remove()
    {
      if (currentItem != null && Utils.DeleteMessage(EntityName))
      {
        DataCollection.Remove(currentItem);
        if (!useSaveButton)
          Save();
      }
    }

    protected virtual void SaveData()
    {
      if (StartShowing != null)
        StartShowing("保存数据");
      Task.Factory.StartNew(() =>
      {
        try
        {
          Save();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message);
        }
        finally
        {
          if (StopShowing != null)
            StopShowing();
        }
      });
    }
    #endregion

    public abstract void LoadData();

    public abstract void Save();

    #region commands

    public DelegateCommand RefreshCommand
    {
      get { return refreshCommand; }
    }

    public DelegateCommand AddCommand
    {
      get { return addCommand; }
    }

    public DelegateCommand ModifyCommand
    {
      get { return modifyCommand; }
    }

    public DelegateCommand DeleteCommand
    {
      get { return deleteCommand; }
    }

    public DelegateCommand SaveCommand
    {
      get { return saveCommand; }
    }

    #endregion

    #region properties

    public EsuInfoCollection<T> DataCollection
    {
      get { return dataCollection; }
      set
      {
        if (Equals(value, dataCollection)) return;
        dataCollection = value;
        NotifyOfPropertyChange(() => DataCollection);
      }
    }

    public T CurrentItem
    {
      get { return currentItem; }
      set
      {
        if (Equals(value, currentItem)) return;
        currentItem = value;
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }

    #endregion
  }
}