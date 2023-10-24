using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetTypeServices : IAssetTypeServices
{
    public readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;

    public AssetTypeServices(IGenericRepository<AssetTypeEntity> assetTypeRepo)
    {
        _assetTypeRepo = assetTypeRepo;
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
        await CheckForDuplicateAssetType(assetType);
        await _assetTypeRepo.CreateAsync(assetType);
    }
    public async Task Update(AssetTypeEntity assetType)
    {
        await CheckForDuplicateAssetType(assetType);
        await _assetTypeRepo.UpdateAsync(assetType);
    }
    public async Task Delete(Guid assetTypeId)
    {
        await _assetTypeRepo.DeleteSoftAsync(assetTypeId);
    }
    public async Task CheckForDuplicateAssetType(AssetTypeEntity assetType)
    {
        var checkAssetType = await _assetTypeRepo.FirstOrDefaultAsync(
            aT => aT.Name.Equals(assetType.Name) && aT.GameId.Equals(assetType.GameId));
        if(checkAssetType is not null)
        {
            if(assetType.Id == Guid.Empty || checkAssetType.Id != assetType.Id)
            {
                throw new BadRequestException(Constants.ENTITY.ASSET_TYPE + Constants.ERROR.ALREADY_EXIST_ERROR);
            }
        }
    }
}
