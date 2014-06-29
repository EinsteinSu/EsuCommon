using System;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.Entities.BasesEntities
{
  public class UpdateFile : EsuInfoBase, IComparable<UpdateFile>
  {
    private string directory;
    private string fileName;
    private DateTime lastWriteTime;
    private string size;

    #region properties

    public string FileName
    {
      get { return fileName; }
      set
      {
        if (value == fileName) return;
        fileName = value;
        NotifyOfPropertyChange(() => FileName);
      }
    }

    public string Directory
    {
      get { return directory; }
      set
      {
        if (value == directory) return;
        directory = value;
        NotifyOfPropertyChange(() => Directory);
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

    #endregion

    #region operators

    public static bool operator <(UpdateFile file1, UpdateFile file2)
    {
      if (file1 == null || file2 == null)
        return false;
      return file1.LastWriteTime.Subtract(file2.LastWriteTime).TotalSeconds < 0;
    }


    public static bool operator >(UpdateFile file1, UpdateFile file2)
    {
      return !(file1 < file2);
    }

    #endregion

    public int CompareTo(UpdateFile other)
    {
      return (int) LastWriteTime.Subtract(other.LastWriteTime).TotalMinutes;
    }
  }

  public class UpdateFileCollection : EsuInfoCollection<UpdateFile>
  {
  }
}