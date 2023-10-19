using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetServices
{
    Task<ICollection<AssetEntity>> List();
    Task<AssetEntity> GetById(Guid assetId); // Get By AssetId
    Task<ICollection<AssetEntity>> GetByAssetTypeId(Guid assetTypeid); // Get By AssetTypeId
    Task<int> Count();
    Task Create(AssetEntity asset);
    Task Update(AssetEntity asset);
    Task Delete(Guid assetId);
}
