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
        return await _assetRepo.ListAsync();
    }
    public async Task<AssetEntity> GetById(Guid assetId)
    {
        return await _assetRepo.FindByIdAsync(assetId);
    }
    public async Task<ICollection<AssetEntity>> GetByAssetTypeId(Guid assetTypeid)
    {
        return await _assetRepo.WhereAsync(
            a => a.AssetTypeId.Equals(assetTypeid));
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
