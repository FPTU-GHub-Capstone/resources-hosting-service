using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetAttributeServices
{
    Task<ICollection<AssetAttributeEntity>> List();
    Task<AssetAttributeEntity> GetById(Guid assetAttributeId);
    Task<ICollection<AssetAttributeEntity>> GetByAssetId(Guid assetId);
    Task<ICollection<AssetAttributeEntity>> GetByAttGroupId(Guid attributeGroupId);
    Task<int> Count();
    Task Create(AssetAttributeEntity assetAttribute);
    Task Update(AssetAttributeEntity assetAttribute);
    Task Delete(Guid assetAttributeId);

}
