using Application.Interfaces;
using Domain.Entities.Attribute;

namespace Application.Services.AttributeServices;

public class AttributeServices : IAttributeServices
{
    public readonly IGenericRepository<AttributeGroup> _attributeRepo;
    public AttributeServices(IGenericRepository<AttributeGroup> attributeRepo)
    {
        _attributeRepo = attributeRepo;
    }
    public async Task<ICollection<AttributeGroup>> GetAttributeGroups()
    {
        var attGrps = await _attributeRepo.ListAsync();
        return attGrps;
    }
    public async Task<AttributeGroup> GetAttributeGroup(Guid attributeGroupid)
    {
        var attGrps = await _attributeRepo.FindByIdAsync(attributeGroupid);
        if (attGrps == null)
        {
            throw new Exception($"Attribute group not exist");
        }
        else
        {
            return attGrps;
        }
    }
    public async Task<int> CountAttributeGroups()
    {
        var attGrps = await _attributeRepo.ListAsync();
        return attGrps.Count;
    }
    public async Task CreateAttributeGroup(AttributeGroup attributeGroup)
    {
        await _attributeRepo.CreateAsync(attributeGroup);
    }
    public async Task UpdateAttributeGroup(Guid attributeGroupid, AttributeGroup attributeGroup)
    {

    }
    public async Task DeleteAttributeGroup(Guid attributeGroupid)
    {

    }
}
