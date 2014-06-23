using Supeng.Common.Entities.BasesEntities;

namespace Supeng.Common.Interfaces
{
  public interface IUpgrade
  {
    EsuUpgradeInfoCollection GetServiceFileCollection(string directoryName);

    EsuUpgradeInfoCollection GetLocalFileCollection(string path);

    byte[] GetFileBytes(string fileName);
  }
}