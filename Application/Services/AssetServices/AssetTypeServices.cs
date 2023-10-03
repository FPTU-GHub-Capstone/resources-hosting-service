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
        var assTyp = await _assetTypeRepo.ListAsync();
        return assTyp;
    }
    public async Task<AssetType> GetById(Guid assetTypeId)
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
    public async Task<ICollection<AssetType>> GetByGameId(Guid gameId)
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
