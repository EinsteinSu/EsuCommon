using System.ComponentModel;
using Supeng.Silverlight.Controls.Text.Annotations;

namespace Supeng.Silverlight.Controls.Text
{
  public partial class MainPage
  {
    public MainPage()
    {
      InitializeComponent();

      DataContext = new TestData
      {
        Text = "Test1111"
      };
    }
  }

  internal sealed class TestData : INotifyPropertyChanged
  {
    private string text;

    public string Text
    {
      get { return text; }
      set
      {
        if (value == text) return;
        text = value;
        OnPropertyChanged("Text");
      }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
