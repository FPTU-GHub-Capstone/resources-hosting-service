using Domain.Entities.Asset;

namespace Application.Interfaces;

public interface IAssetServices
{
    //Asset Attributes
    Task<ICollection<AssetAttribute>> GetAssetAttributes();
    Task<AssetAttribute> GetAssetAttribute(Guid assetAttributeId); // Get By AssetAttributeId
    Task<ICollection<AssetAttribute>> GetAssetAttributes(Guid id, int typeId); // typeId: 1: AssetId, 2: AttributeGroupId
    Task<int> CountAssetAttributes();
    Task CreateAssetAttribute(AssetAttribute assetAttribute);
    Task UpdateAssetAttribute(Guid assetAttributeId, AssetAttribute assetAttribute);
    Task DeleteAssetAttribute(Guid assetAttributeId);

    //Asset
    Task<ICollection<AssetEntity>> GetAssets();
    Task<AssetEntity> GetAsset(Guid assetId); // Get By AssetId
    Task<ICollection<AssetEntity>> GetAssets(Guid assetTypeid); // Get By AssetTypeId
    Task<int> CountAssets();
    Task CreateAsset(AssetEntity asset);
    Task UpdateAsset(Guid assetId, AssetEntity asset);
    Task DeleteAsset(Guid assetId);

    //Asset Types
    Task<ICollection<AssetType>> GetAssetTypes();
    Task<AssetType> GetAssetType(Guid assetTypeId); // Get By AssetTypeId
    Task<ICollection<AssetType>> GetAssetTypes(Guid gameId); // Get By GameId
    Task<int> CountAssetTypes();
    Task CreateAssetType(AssetType assetType);
    Task UpdateAssetType(Guid assetTypeId, AssetType assetType);
    Task DeleteAssetType(Guid assetTypeId);
}
