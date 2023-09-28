using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Asset;
using System.Collections.ObjectModel;

namespace Application.Services.AssetServices;

public class AssetServices : IAssetServices
{
    public readonly IGenericRepository<AssetAttribute> _assetAttributeRepo;
    public readonly IGenericRepository<AssetEntity> _assetRepo;
    public readonly IGenericRepository<AssetType> _assetTypeRepo;

    public AssetServices(IGenericRepository<AssetAttribute> assetAttributeRepo, IGenericRepository<AssetEntity> assetRepo, IGenericRepository<AssetType> assetTypeRepo)
    {
        assetAttributeRepo = _assetAttributeRepo;
        assetRepo = _assetRepo;
        assetTypeRepo = _assetTypeRepo;
    }

    //Asset Attributes
    public async Task<ICollection<AssetAttribute>> GetAssetAttributes() {
        var assAtt = await _assetAttributeRepo.ListAsync();
        return assAtt;
    }
    public async Task<AssetAttribute> GetAssetAttribute(Guid assetAttributeId)
    { // Get By AssetAttributeId
        var assAtt = await _assetAttributeRepo.FindByIdAsync(assetAttributeId);
        if(assAtt == null)
        {
            throw new Exception($"Asset attribute not exist");
        }
        else
        {
            return assAtt;
        }
    }
    public async Task<ICollection<AssetAttribute>> GetAssetAttributes(Guid id, int typeId)
    { // typeId: 1: AssetId, 2: AttributeGroupId
        ICollection<AssetAttribute> assAtt = new Collection<AssetAttribute>();
        if(typeId == 1)
        {
            assAtt = await _assetAttributeRepo.WhereAsync(
                a => a.AssetId.Equals(id));
        }
        else if(typeId == 2)
        {
            assAtt = await _assetAttributeRepo.WhereAsync(
                a => a.AttributeGroupId.Equals(id));
        }
        //Return if exist
        if(assAtt.Count == 0 || assAtt == null)
        {
            throw new Exception($"Asset attribute or Asset ID/ Attribute Group not found");
        }
        else
        {
            return assAtt;
        }
    }
    public async Task<int> CountAssetAttributes()
    {
        var assAtt = await _assetAttributeRepo.ListAsync();
        return assAtt.Count;
    }
    public async Task CreateAssetAttribute(AssetAttribute assetAttribute)
    {

    }
    public async Task UpdateAssetAttribute(Guid assetAttributeId, AssetAttribute assetAttribute)
    {

    }
    public async Task DeleteAssetAttribute(Guid assetAttributeId)
    {

    }

    //Asset
    public async Task<ICollection<AssetEntity>> GetAssets()
    {
        var assets = await _assetRepo.ListAsync();
        return assets;
    }
    public async Task<AssetEntity> GetAsset(Guid assetId)
    { // Get By AssetId
        var asset = await _assetRepo.FindByIdAsync(assetId);
        if (asset == null)
        {
            throw new Exception($"Asset not exist");
        }
        else
        {
            return asset;
        }
    }
    public async Task<ICollection<AssetEntity>> GetAssets(Guid assetTypeid)
    { // Get By AssetTypeId
        var assets = await _assetRepo.WhereAsync(
            a => a.AssetTypeId.Equals(assetTypeid));
        if (assets.Count == 0 || assets == null)
        {
            throw new Exception($"Asset or Asset type not found");
        }
        else
        {
            return assets;
        }
    }
    public async Task<int> CountAssets()
    {
        var assets = await _assetRepo.ListAsync();
        return assets.Count;
    }
    public async Task CreateAsset(AssetEntity asset)
    {

    }
    public async Task UpdateAsset(Guid assetId, AssetEntity asset)
    {

    }
    public async Task DeleteAsset(Guid assetId)
    {

    }

    //Asset Types
    public async Task<ICollection<AssetType>> GetAssetTypes()
    {
        var assTyp = await _assetTypeRepo.ListAsync();
        return assTyp;
    }
    public async Task<AssetType> GetAssetType(Guid assetTypeId)
    { // Get By AssetTypeId
        var assTyp = await _assetTypeRepo.FindByIdAsync(assetTypeId);
        if (assTyp == null)
        {
            throw new Exception($"Asset type not exist");
        }
        else
        {
            return assTyp;
        }
    }
    public async Task<ICollection<AssetType>> GetAssetTypes(Guid gameId)
    { // Get By GameId
        var assTyp = await _assetTypeRepo.WhereAsync(
            a=>a.GameId.Equals(gameId));
        if (assTyp.Count == 0 || assTyp == null)
        {
            throw new Exception($"Asset type or Game not found");
        }
        else
        {
            return assTyp;
        }
    }
    public async Task<int> CountAssetTypes()
    {
        var assTyp = await _assetTypeRepo.ListAsync();
        return assTyp.Count;
    }
    public async Task CreateAssetType(AssetType assetType)
    {
        
    }
    public async Task UpdateAssetType(Guid assetTypeId, AssetType assetType)
    {

    }
    public async Task DeleteAssetType(Guid assetTypeId)
    {

    }
}
