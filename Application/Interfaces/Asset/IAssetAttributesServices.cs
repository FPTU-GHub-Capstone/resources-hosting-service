using Domain.Entities.Asset;

namespace Application.Interfaces;

public interface IAssetAttributeServices
{
    Task<ICollection<AssetAttribute>> List();
    Task<AssetAttribute> GetById(Guid assetAttributeId); // Get By AssetAttributeId
    Task<ICollection<AssetAttribute>> GetByAssetId(Guid assetId);
    Task<ICollection<AssetAttribute>> GetByAttGroupId(Guid attributeGroupId);
    Task<int> Count();
    Task Create(AssetAttribute assetAttribute);
    Task Update(Guid assetAttributeId, AssetAttribute assetAttribute);
    Task Delete(Guid assetAttributeId);

}
