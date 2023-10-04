using DomainLayer.Entities;

namespace ServiceLayer.Business.AssetAttribute;

public interface IAssetAttributeServices
{
    Task<ICollection<AssetAttributeEntity>> List();
    Task<AssetAttributeEntity> GetById(Guid assetAttributeId); // Get By AssetAttributeId
    Task<ICollection<AssetAttributeEntity>> GetByAssetId(Guid assetId);
    Task<ICollection<AssetAttributeEntity>> GetByAttGroupId(Guid attributeGroupId);
    Task<int> Count();
    Task Create(AssetAttributeEntity assetAttribute);
    Task Update(Guid assetAttributeId, AssetAttributeEntity assetAttribute);
    Task Delete(Guid assetAttributeId);

}
