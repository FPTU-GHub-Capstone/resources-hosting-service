using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AssetAttributeServices : IAssetAttributeServices
{
    private readonly IGenericRepository<AssetAttributeEntity> _assetAttributeRepo;
    private readonly IAssetServices _assetServices;
    private readonly IAttributeGroupServices _attributeGroupServices;

    public AssetAttributeServices(IGenericRepository<AssetAttributeEntity> assetAttributeRepo, IAssetServices assetService
        , IAttributeGroupServices attributeGroupService)
    {
        _assetAttributeRepo = assetAttributeRepo;
        _assetServices = assetService;
        _attributeGroupServices = attributeGroupService;
    }
    public async Task<ICollection<AssetAttributeEntity>> List()
    {
        return await _assetAttributeRepo.ListAsync();
    }
    public async Task<AssetAttributeEntity> GetById(Guid assetAttributeId)
    {
        return await _assetAttributeRepo.FoundOrThrowAsync(assetAttributeId, Constants.Entities.ASSET_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<AssetAttributeEntity>> ListAssetAttributeByGameId(Guid id)
    {
        var assetIds = (await _assetServices.ListAssetsByGameId(id)).Select(x => x.Id);
        var attributeIds = (await _attributeGroupServices.ListAttributeGroupsByGameId(id)).Select(x => x.Id);
        return await _assetAttributeRepo.WhereAsync(a=> attributeIds.Contains(a.AttributeGroupId) 
                                            || assetIds.Contains(a.AssetId));
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
            throw new BadRequestException(Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.ALREADY_EXIST_ERROR);
        }
    }
}