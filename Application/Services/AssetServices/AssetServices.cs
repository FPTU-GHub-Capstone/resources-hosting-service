using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Asset;
using System.Collections.ObjectModel;

namespace Application.Services.AssetServices;

public class AssetServices : IAssetServices
{
    public readonly IGenericRepository<AssetEntity> _assetRepo;

    public AssetServices(IGenericRepository<AssetEntity> assetRepo)
    {
        assetRepo = _assetRepo;
    }
    public async Task<ICollection<AssetEntity>> List()
    {
        var assets = await _assetRepo.ListAsync();
        return assets;
    }
    public async Task<AssetEntity> GetById(Guid assetId)
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
    public async Task<ICollection<AssetEntity>> GetByAssetTypeId(Guid assetTypeid)
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
    public async Task<int> Count()
    {
        return await _assetRepo.CountAsync();
    }
    public async Task Create(AssetEntity asset)
    {

    }
    public async Task Update(Guid assetId, AssetEntity asset)
    {

    }
    public async Task Delete(Guid assetId)
    {

    }
}
