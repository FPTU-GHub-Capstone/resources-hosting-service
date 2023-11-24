using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetTypeServices
{
    Task<ICollection<AssetTypeEntity>> List();
    Task<AssetTypeEntity> GetById(Guid assetTypeId);
    Task<ICollection<AssetTypeEntity>> ListAssTypesByGameId(Guid gameId);
    Task<int> Count();
    Task Create(AssetTypeEntity assetType);
    Task Update(AssetTypeEntity assetType);
    Task Delete(Guid assetTypeId);
}
