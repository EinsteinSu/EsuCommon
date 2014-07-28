using System.Windows;
using Supeng.Silverlight.Controls.Extensions;
using Supeng.Silverlight.Controls.ViewModels;

namespace Supeng.Silverlight.Controls.Views
{
  public partial class PreviewTextControl
  {
    public PreviewTextControl()
    {
      InitializeComponent();
    }

    public readonly static DependencyProperty MemoTextDependency =
        DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(PreviewTextControl),
            new PropertyMetadata(Target)
            );

    private static void Target(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var text = sender as PreviewTextControl;
      if (text != null)
        text.edit.Text = e.NewValue.ToString();
    }

    public string Text
    {
      get
      {
        return (string)GetValue(MemoTextDependency);
      }
      set
      {
        SetValue(MemoTextDependency, value);
      }
    }

    private void PreviewClick(object sender, RoutedEventArgs e)
    {
      DialogWindowHelper.ShowDialogWindow(new MemoTextWindowViewModel(new MemoTextControl(), Text), null);
    }
  }
}
