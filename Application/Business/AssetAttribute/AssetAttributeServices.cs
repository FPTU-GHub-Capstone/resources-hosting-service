using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetAttributeServices : IAssetAttributeServices
{
    public readonly IGenericRepository<AssetAttributeEntity> _assetAttributeRepo;

    public AssetAttributeServices(IGenericRepository<AssetAttributeEntity> assetAttributeRepo)
    {
        _assetAttributeRepo = assetAttributeRepo;
    }

    public async Task<ICollection<AssetAttributeEntity>> List()
    {
        return await _assetAttributeRepo.ListAsync();
    }
    public async Task<AssetAttributeEntity> GetById(Guid assetAttributeId)
    {
        return await _assetAttributeRepo.FoundOrThrowAsync(assetAttributeId, Constants.ENTITY.ASSET_ATTRIBUTE + Constants.ERROR.NOT_EXIST_ERROR);
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
        await CheckForDuplicateAssetAttribute(assetAttribute);
        await _assetAttributeRepo.CreateAsync(assetAttribute);
    }
    public async Task Update(AssetAttributeEntity assetAttribute)
    {
        await _assetAttributeRepo.UpdateAsync(assetAttribute);
    }
    public async Task Delete(Guid assetAttributeId)
    {
        await _assetAttributeRepo.DeleteSoftAsync(assetAttributeId);
    }
    public async Task CheckForDuplicateAssetAttribute(AssetAttributeEntity assAtt)
    {
        var checkAssAtt = await _assetAttributeRepo.FirstOrDefaultAsync(
            l => l.AssetId == assAtt.AssetId && l.AttributeGroupId == assAtt.AttributeGroupId);
        if (checkAssAtt is not null && (assAtt.Id == Guid.Empty || checkAssAtt.Id != assAtt.Id))
        {
            throw new BadRequestException(Constants.ENTITY.ATTRIBUTE_GROUP + Constants.ERROR.ALREADY_EXIST_ERROR);
        }
    }
}