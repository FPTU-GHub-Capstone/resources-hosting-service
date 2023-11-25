using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAssetAttributeServices
{
    Task<ICollection<AssetAttributeEntity>> List();
    Task<AssetAttributeEntity> GetById(Guid assetAttributeId);
    Task Create(AssetAttributeEntity assetAttribute);
    Task Update(AssetAttributeEntity assetAttribute);
    Task Delete(Guid assetAttributeId);

}
