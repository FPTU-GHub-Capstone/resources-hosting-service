using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetServices
{
    Task<ICollection<AssetEntity>> List();
    Task<AssetEntity> GetById(Guid assetId);
    Task<ICollection<AssetEntity>> ListAssetsByGameId(Guid gameId);
    Task Create(AssetEntity asset);
    Task Update(AssetEntity asset);
    Task Delete(Guid assetId);
}
