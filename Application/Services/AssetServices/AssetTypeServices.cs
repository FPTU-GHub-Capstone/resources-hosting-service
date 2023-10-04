using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Asset;
using System.Collections.ObjectModel;

namespace Application.Services.AssetServices;

public class AssetTypeServices : IAssetTypeServices
{
    public readonly IGenericRepository<AssetType> _assetTypeRepo;

    public AssetTypeServices(IGenericRepository<AssetType> assetTypeRepo)
    {
        assetTypeRepo = _assetTypeRepo;
    }
    public async Task<ICollection<AssetType>> List()
    {
        return await _assetTypeRepo.ListAsync();
    }
    public async Task<AssetType> GetById(Guid assetTypeId)
    {
        return await _assetTypeRepo.FindByIdAsync(assetTypeId);
    }
    public async Task<ICollection<AssetType>> GetByGameId(Guid gameId)
    {
        return await _assetTypeRepo.WhereAsync(
            a=>a.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _assetTypeRepo.CountAsync();
    }
    public async Task Create(AssetType assetType)
    {
        
    }
    public async Task Update(Guid assetTypeId, AssetType assetType)
    {

    }
    public async Task Delete(Guid assetTypeId)
    {

    }
}
