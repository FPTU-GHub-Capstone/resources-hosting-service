using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Activity;
using System.Collections.ObjectModel;

namespace Application.Services.AssetServices;

public class AssetAttributeServices : IAssetAttributeServices
{
    public readonly IGenericRepository<AssetAttribute> _assetAttributeRepo;

    public AssetAttributeServices(IGenericRepository<AssetAttribute> assetAttributeRepo)
    {
        assetAttributeRepo = _assetAttributeRepo;
    }

    public async Task<ICollection<AssetAttribute>> List() {
        return await _assetAttributeRepo.ListAsync();
    }
    public async Task<AssetAttribute> GetById(Guid assetAttributeId)
    { 
        return await _assetAttributeRepo.FindByIdAsync(assetAttributeId);
    }
    public async Task<ICollection<AssetAttribute>> GetByAssetId(Guid assetId)
    {
        return await _assetAttributeRepo.WhereAsync(a=>a.AssetId == assetId);
    }
    public async Task<ICollection<AssetAttribute>> GetByAttGroupId(Guid attributeGroupId)
    {
        return await _assetAttributeRepo.WhereAsync(a => a.AttributeGroupId == attributeGroupId);
    }
    public async Task<int> Count()
    {
        return await _assetAttributeRepo.CountAsync();
    }
    public async Task Create(AssetAttribute assetAttribute)
    {

    }
    public async Task Update(Guid assetAttributeId, AssetAttribute assetAttribute)
    {

    }
    public async Task Delete(Guid assetAttributeId)
    {

    }
}
