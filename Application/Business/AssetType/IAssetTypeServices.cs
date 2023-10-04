using DomainLayer.Entities;

namespace ServiceLayer.Business.AssetType;

public interface IAssetTypeServices
{
    Task<ICollection<AssetTypeEntity>> List();
    Task<AssetTypeEntity> GetById(Guid assetTypeId);
    Task<ICollection<AssetTypeEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(AssetTypeEntity assetType);
    Task Update(Guid assetTypeId, AssetTypeEntity assetType);
    Task Delete(Guid assetTypeId);
}
