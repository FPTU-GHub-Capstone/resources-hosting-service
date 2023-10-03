using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Asset;
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
        var assAtt = await _assetAttributeRepo.ListAsync();
        return assAtt;
    }
    public async Task<AssetAttribute> GetById(Guid assetAttributeId)
    { // Get By AssetAttributeId
        var assAtt = await _assetAttributeRepo.FindByIdAsync(assetAttributeId);
        if(assAtt == null)
        {
            throw new Exception($"Asset attribute not exist");
        }
        else
        {
            return assAtt;
        }
    }
    public async Task<ICollection<AssetAttribute>> GetById(Guid id, int typeId)
    { // typeId: 1: AssetId, 2: AttributeGroupId
        ICollection<AssetAttribute> assAtt = new Collection<AssetAttribute>();
        if(typeId == 1)
        {
            assAtt = await _assetAttributeRepo.WhereAsync(
                a => a.AssetId.Equals(id));
        }
        else if(typeId == 2)
        {
            assAtt = await _assetAttributeRepo.WhereAsync(
                a => a.AttributeGroupId.Equals(id));
        }
        //Return if exist
        if(assAtt.Count == 0 || assAtt == null)
        {
            throw new Exception($"Asset attribute or Asset ID/ Attribute Group not found");
        }
        else
        {
            return assAtt;
        }
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
