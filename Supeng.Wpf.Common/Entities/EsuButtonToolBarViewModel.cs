using System.Collections.ObjectModel;
using System.Windows;
using Supeng.Wpf.Common.Templates;

namespace Supeng.Wpf.Common.Entities
{
  public class EsuToolBarButtonCollection : ObservableCollection<EsuButtonBase>
  {
    private readonly EsuButtonToolBarType toolBarType;

    public EsuToolBarButtonCollection(EsuButtonToolBarType toolBarType)
    {
      this.toolBarType = toolBarType;
    }

    public EsuToolBarButtonCollection Collection
    {
      get { return this; }
    }

    public DataTemplate Template
    {
      get { return toolBarType.GetDataTemplate(); }
    }
  }

  public enum EsuButtonToolBarType
  {
    TextAndImage,
    OnlyText,
    OnlyImage
  }

  public static class EsuButtonToolBarTypeExtensions
  {
    public static DataTemplate GetDataTemplate(this EsuButtonToolBarType type)
    {
      return type.ToString().GetDataTemplateFromFile<DataTemplate>();
    }
  }
}