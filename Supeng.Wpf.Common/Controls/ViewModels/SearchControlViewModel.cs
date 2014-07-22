using System.IO;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Common.IOs;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class SearchControlViewModel : EsuInfoBase
  {
    private readonly DelegateCommand clearCommand;
    private readonly DelegateCommand searchCommand;

    protected SearchControlViewModel()
    {
      searchCommand = new DelegateCommand(Search, () => true);
      clearCommand = new DelegateCommand(Clear, () => true);
    }

    protected abstract string TemplateName { get; }

    public abstract FrameworkElement Content { get; }

    public abstract ISearchModel Data { get; set; }

    public DelegateCommand SearchCommand
    {
      get { return searchCommand; }
    }

    public DelegateCommand ClearCommand
    {
      get { return clearCommand; }
    }

    protected T LoadSearchModel<T>()
    {
      string templateFileName = string.Format("{0}{1}.txt", DirectoryHelper.TemplateDirectory, TemplateName);
      if (File.Exists(templateFileName))
      {
        try
        {
          return templateFileName.LoadDataFromText<T>();
        }
        catch
        {
          return default(T);
        }
      }
      return default(T);
    }

    protected virtual void SaveTemplate()
    {
      string templateFileName = string.Format("{0}{1}.txt", DirectoryHelper.TemplateDirectory, TemplateName);
      if (Data != null)
      {
        var info = Data as EsuInfoBase;
        if (info != null)
          info.SerializeToText(templateFileName);
      }
    }

    protected abstract void Search();

    protected virtual void Clear()
    {
      if (Data != null)
        Data.Clear();
    }
  }
}