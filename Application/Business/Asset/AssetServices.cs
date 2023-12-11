using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetServices : IAssetServices
{
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    private readonly IAssetTypeServices _assetTypeService;

    public AssetServices(IGenericRepository<AssetEntity> assetRepo, IAssetTypeServices assetTypeService)
    {
        _assetRepo = assetRepo;
        _assetTypeService = assetTypeService;
    }
    public async Task<ICollection<AssetEntity>> List()
    {
        return await _assetRepo.ListAsync();
    }
    public async Task<AssetEntity> GetById(Guid assetId)
    {
        return await _assetRepo.FoundOrThrowAsync(assetId, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
    }

    public async Task<ICollection<AssetEntity>> ListAssetsByGameId(Guid gameId)
    {
        var assetTypeIds = (await _assetTypeService.ListAssTypesByGameId(gameId)).Select(a=>a.Id);
        return await _assetRepo.WhereAsync(a => assetTypeIds.Contains(a.AssetTypeId));
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
