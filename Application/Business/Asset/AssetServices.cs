using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetServices : IAssetServices
{
    public readonly IGenericRepository<AssetEntity> _assetRepo;

    public AssetServices(IGenericRepository<AssetEntity> assetRepo)
    {
        _assetRepo = assetRepo;
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
        await _assetRepo.CreateAsync(asset);
    }
    public async Task Update(AssetEntity asset)
    {
        await _assetRepo.UpdateAsync(asset);
    }
    public async Task Delete(Guid assetId)
    {
        await _assetRepo.DeleteSoftAsync(assetId);
    }
}
