using Domain.Entities.Asset;

namespace Application.Interfaces;

public interface IAssetAttributeServices
{
    //Asset Attributes
    Task<ICollection<AssetAttribute>> List();
    Task<AssetAttribute> GetById(Guid assetAttributeId); // Get By AssetAttributeId
    Task<ICollection<AssetAttribute>> GetById(Guid id, int typeId); // typeId: 1: AssetId, 2: AttributeGroupId
    Task<int> Count();
    Task Create(AssetAttribute assetAttribute);
    Task Update(Guid assetAttributeId, AssetAttribute assetAttribute);
    Task Delete(Guid assetAttributeId);

}
