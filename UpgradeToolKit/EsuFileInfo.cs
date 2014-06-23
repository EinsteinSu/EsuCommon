using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;

namespace UpgradeToolKit
{
  public class EsuFileInfo : PropertyChangedBase
  {
    private readonly string root;
    private string fileName;
    private DateTime lastWriteTime;
    private string name;
    private string relativeFileName;
    private string size;
    private FileType type;

    public EsuFileInfo()
    {
    }

    public EsuFileInfo(string root)
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
        NotifyOfPropertyChange(() => Type);
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

    public string FileName
    {
      get { return fileName; }
      set
      {
        if (value == fileName) return;
        fileName = value;
        NotifyOfPropertyChange(() => FileName);
        NotifyOfPropertyChange(() => RelativeFileName);
      }
    }

    public DateTime LastWriteTime
    {
      get { return lastWriteTime; }
      set
      {
        if (value.Equals(lastWriteTime)) return;
        lastWriteTime = value;
        NotifyOfPropertyChange(() => LastWriteTime);
      }
    }

    public string Size
    {
      get { return size; }
      set
      {
        if (value == size) return;
        size = value;
        NotifyOfPropertyChange(() => Size);
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
        NotifyOfPropertyChange(() => RelativeFileName);
      }
    }
  }

  public class EsuFileInfoCollection : ObservableCollection<EsuFileInfo>
  {
    public IList<EsuFileInfo> GetDirectoryList()
    {
      return this.Where(w => w.Type == FileType.Directory).ToList();
    }

    public IList<EsuFileInfo> GetFileList()
    {
      return this.Where(w => w.Type == FileType.File).ToList();
    }

    public IList<EsuFileInfo> GetDifferentFileList(IList<EsuFileInfo> collection)
    {
      IEnumerable<EsuFileInfo> c = from data in GetFileList()
        where !collection.Select(s => s.RelativeFileName).Contains(data.RelativeFileName)
        select data;
      List<EsuFileInfo> list = c.ToList();
      foreach (EsuFileInfo esuFileInfo in this)
      {
        EsuFileInfo item = collection.FirstOrDefault(f => f.RelativeFileName == esuFileInfo.RelativeFileName);
        if (item != null && item.LastWriteTime < esuFileInfo.LastWriteTime)
        {
          list.Add(item);
        }
      }
      return list;
    }

    public IList<EsuFileInfo> GetDifferentDirectoryList(IList<EsuFileInfo> collection)
    {
      IEnumerable<EsuFileInfo> c = from data in GetDirectoryList()
        where !collection.Select(s => s.RelativeFileName).Contains(data.RelativeFileName)
        select data;
      return c.ToList();
    }

    public IList<EsuFileInfo> GetDifferent(EsuFileInfoCollection collection)
    {
      List<EsuFileInfo> list = GetDifferentDirectoryList(collection.GetDirectoryList()).ToList();
      list.AddRange(GetDifferentFileList(collection.GetFileList()));
      return list.ToList();
    }
  }

  public enum FileType
  {
    Directory,
    File
  }
}