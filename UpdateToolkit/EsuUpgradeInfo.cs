using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UpdateToolkit
{
  public class EsuUpgradeInfo : INotifyPropertyChanged
  {
    private readonly string root;
    private string fileName;
    private DateTime lastWriteTime;
    private string name;
    private string relativeFileName;
    private string size;
    private FileType type;

    public EsuUpgradeInfo()
    {
    }

    public EsuUpgradeInfo(string root)
    {
      this.root = root;
    }

    public FileType Type
    {
      get { return type; }
      set
      {
        if (value == type) return;
        type = value;
        OnPropertyChanged();
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        OnPropertyChanged();
      }
    }

    public string FileName
    {
      get { return fileName; }
      set
      {
        if (value == fileName) return;
        fileName = value;
        OnPropertyChanged();
      }
    }

    public DateTime LastWriteTime
    {
      get { return lastWriteTime; }
      set
      {
        if (value.Equals(lastWriteTime)) return;
        lastWriteTime = value;
        OnPropertyChanged();
      }
    }

    public string Size
    {
      get { return size; }
      set
      {
        if (value == size) return;
        size = value;
        OnPropertyChanged();
      }
    }

    public string RelativeFileName
    {
      get
      {
        if (!string.IsNullOrEmpty(root))
          relativeFileName = FileName.Replace(root, "");
        return relativeFileName;
      }
      set
      {
        if (value == relativeFileName) return;
        relativeFileName = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public enum FileType
  {
    Directory,
    File
  }
}