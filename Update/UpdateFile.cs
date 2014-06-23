using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Supeng.Common.Entities;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;

namespace Update
{
  public class UpdateFile : EsuInfoBase
  {
    private EsuUpgradeInfo fileInfo;
    private bool upgradable;

    public BitmapImage Image
    {
      get
      {
        if (fileInfo.Type == FileType.Directory)
          return new BitmapImage(new Uri("Images/Folder.png", UriKind.Relative));
        return new BitmapImage(new Uri("Images/File.png", UriKind.Relative));
      }
    }

    public EsuUpgradeInfo FileInfo
    {
      get { return fileInfo; }
      set
      {
        if (Equals(value, fileInfo)) return;
        fileInfo = value;
        NotifyOfPropertyChange(() => FileInfo);
        NotifyOfPropertyChange(() => Image);
      }
    }

    public bool Upgradable
    {
      get { return upgradable; }
      set
      {
        if (value.Equals(upgradable)) return;
        upgradable = value;
        NotifyOfPropertyChange(() => Upgradable);
        NotifyOfPropertyChange(() => UpgradeImage);
      }
    }

    public BitmapImage UpgradeImage
    {
      get
      {
        if (upgradable)
          return new BitmapImage(new Uri("Images/Ok.png", UriKind.Relative));
        return null;
      }
    }
  }

  public class UpdateFileCollection : EsuInfoCollection<UpdateFile>
  {
    public UpdateFileCollection(IEnumerable<EsuUpgradeInfo> list)
    {
      foreach (var esuFileInfo in list)
      {
        Add(new UpdateFile
        {
          FileInfo = esuFileInfo,
        });
      }
    }
  }
}