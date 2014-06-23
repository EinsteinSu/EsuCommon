using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.Entities.BasesEntities
{
  public class EsuUpgradeInfo : EsuInfoBase
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

    [Display(Name = "类型", Order = 0)]
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

    [Display(Name = "名称", Order = 1)]
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

    [Display(AutoGenerateField = false)]
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

    [Display(Name = "更新时间", Order = 1)]
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

    [Display(Name = "大小", Order = 0)]
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

    [Display(AutoGenerateField = false)]
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

  public class EsuUpgradeInfoCollection : EsuInfoCollection<EsuUpgradeInfo>
  {
    public IList<EsuUpgradeInfo> GetDirectoryList()
    {
      return this.Where(w => w.Type == FileType.Directory).ToList();
    }

    public IList<EsuUpgradeInfo> GetFileList()
    {
      return this.Where(w => w.Type == FileType.File).ToList();
    }

    public IList<EsuUpgradeInfo> GetDifferentFileList(IList<EsuUpgradeInfo> collection)
    {
      IEnumerable<EsuUpgradeInfo> c = from data in GetFileList()
        where !collection.Select(s => s.RelativeFileName).Contains(data.RelativeFileName)
        select data;
      List<EsuUpgradeInfo> list = c.ToList();
      foreach (EsuUpgradeInfo esuFileInfo in this)
      {
        EsuUpgradeInfo item = collection.FirstOrDefault(f => f.RelativeFileName == esuFileInfo.RelativeFileName);
        if (item != null && item.LastWriteTime < esuFileInfo.LastWriteTime)
        {
          list.Add(item);
        }
      }
      return list;
    }

    public IList<EsuUpgradeInfo> GetDifferentDirectoryList(IList<EsuUpgradeInfo> collection)
    {
      IEnumerable<EsuUpgradeInfo> c = from data in GetDirectoryList()
        where !collection.Select(s => s.RelativeFileName).Contains(data.RelativeFileName)
        select data;
      return c.ToList();
    }

    public IList<EsuUpgradeInfo> GetDifferent(EsuUpgradeInfoCollection collection)
    {
      List<EsuUpgradeInfo> list = GetDifferentDirectoryList(collection.GetDirectoryList()).ToList();
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