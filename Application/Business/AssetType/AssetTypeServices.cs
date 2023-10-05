using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetTypeServices : IAssetTypeServices
{
    public readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;

    public AssetTypeServices(IGenericRepository<AssetTypeEntity> assetTypeRepo)
    {
        assetTypeRepo = _assetTypeRepo;
    }
    public async Task<ICollection<AssetTypeEntity>> List()
    {
        return await _assetTypeRepo.ListAsync();
    }
    public async Task<AssetTypeEntity> GetById(Guid assetTypeId)
    {
        return await _assetTypeRepo.FindByIdAsync(assetTypeId);
    }
    public async Task<ICollection<AssetTypeEntity>> GetByGameId(Guid gameId)
    {
        return await _assetTypeRepo.WhereAsync(
            a => a.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _assetTypeRepo.CountAsync();
    }
    public async Task Create(AssetTypeEntity assetType)
    {

    }
    public async Task Update(Guid assetTypeId, AssetTypeEntity assetType)
    {

    }
    public async Task Delete(Guid assetTypeId)
    {

    }
}
