using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UpdateToolkit
{
  public class EsuUpgradeInfoCollection : ObservableCollection<EsuUpgradeInfo>
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
}