using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business.AssetAttribute;

public class AssetAttributeServices : IAssetAttributeServices
{
    public readonly IGenericRepository<AssetAttributeEntity> _assetAttributeRepo;

    public AssetAttributeServices(IGenericRepository<AssetAttributeEntity> assetAttributeRepo)
    {
        assetAttributeRepo = _assetAttributeRepo;
    }

    public async Task<ICollection<AssetAttributeEntity>> List()
    {
        return await _assetAttributeRepo.ListAsync();
    }
    public async Task<AssetAttributeEntity> GetById(Guid assetAttributeId)
    {
        return await _assetAttributeRepo.FindByIdAsync(assetAttributeId);
    }
    public async Task<ICollection<AssetAttributeEntity>> GetByAssetId(Guid assetId)
    {
        return await _assetAttributeRepo.WhereAsync(a => a.AssetId == assetId);
    }
    public async Task<ICollection<AssetAttributeEntity>> GetByAttGroupId(Guid attributeGroupId)
    {
        return await _assetAttributeRepo.WhereAsync(a => a.AttributeGroupId == attributeGroupId);
    }
    public async Task<int> Count()
    {
        return await _assetAttributeRepo.CountAsync();
    }
    public async Task Create(AssetAttributeEntity assetAttribute)
    {

    }
    public async Task Update(Guid assetAttributeId, AssetAttributeEntity assetAttribute)
    {

    }
    public async Task Delete(Guid assetAttributeId)
    {

    }
}
