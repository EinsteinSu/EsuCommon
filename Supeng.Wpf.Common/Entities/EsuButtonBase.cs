using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Common.IOs;

namespace Supeng.Wpf.Common.Entities
{
  public class EsuButtonBase : EsuInfoBase
  {
    private string text;
    private bool enable;
    private Thickness thickness = new Thickness(0);
    private string imageUrl;
    private readonly DelegateCommand command;
    private string name;

    public EsuButtonBase()
    {

    }

    public EsuButtonBase(Action action)
    {
      enable = true;
      command = new DelegateCommand(action, () => true);
    }

    public EsuButtonBase(string text, int left, Action action)
      : this(action)
    {
      this.text = text;
      thickness = new Thickness(left, 0, 0, 0);
      NotifyOfPropertyChange(()=>Thickness);
    }

    #region properties
    public string Text
    {
      get { return text; }
      set
      {
        if (value == text) return;
        text = value;
        NotifyOfPropertyChange(() => Text);
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public bool Enable
    {
      get { return enable; }
      set
      {
        if (value.Equals(enable)) return;
        enable = value;
        NotifyOfPropertyChange(() => Enable);
      }
    }

    public Thickness Thickness
    {
      get { return thickness; }
      set
      {
        if (value.Equals(thickness)) return;
        thickness = value;
        NotifyOfPropertyChange(() => Thickness);
      }
    }

    public Thickness ContextMenuThickness
    {
      get
      {
        return new Thickness(0, thickness.Left, 0, 0);
      }
    }

    public string ImageUrl
    {
      get
      {
        if (string.IsNullOrEmpty(imageUrl))
          return string.Format("{0}Buttons\\{1}.png", DirectoryHelper.ImageDirectory, name);
        return imageUrl;
      }
      set
      {
        if (value == imageUrl) return;
        imageUrl = value;
        NotifyOfPropertyChange(() => ImageUrl);
      }
    }

    public ImageSource Image
    {
      get
      {
        if (File.Exists(ImageUrl))
          return new BitmapImage(new Uri(ImageUrl, UriKind.Absolute));
        return null;
      }
    }

    public DelegateCommand Command
    {
      get { return command; }
    }
    #endregion
  }
}
