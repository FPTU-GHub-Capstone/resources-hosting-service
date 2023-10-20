using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetServices
{
    Task<ICollection<AssetEntity>> List();
    Task<AssetEntity> GetById(Guid assetId);
    Task<ICollection<AssetEntity>> GetByAssetTypeId(Guid assetTypeid);
    Task<int> Count();
    Task Create(AssetEntity asset);
    Task Update(AssetEntity asset);
    Task Delete(Guid assetId);
}
