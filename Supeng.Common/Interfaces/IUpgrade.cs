using Supeng.Common.Entities.BasesEntities;

namespace Supeng.Common.Interfaces
{
  public interface IUpgrade
  {
    EsuUpgradeInfoCollection GetServiceFileCollection();

    EsuUpgradeInfoCollection GetLocalFileCollection();

    byte[] GetFileBytes(string fileName);
  }
}