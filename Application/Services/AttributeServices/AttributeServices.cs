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
    public async Task<ICollection<AttributeGroup>> List()
    {
        var attGrps = await _attributeRepo.ListAsync();
        return attGrps;
    }
    public async Task<AttributeGroup> GetById(Guid attributeGroupid)
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
    public async Task<int> Count()
    {
        return await _attributeRepo.CountAsync();
    }
    public async Task Create(AttributeGroup attributeGroup)
    {
        var attList = List();
        var check = false;
        foreach (var attgrp in attList.Result)
        {
            if (attgrp.Name == attributeGroup.Name)
            {
                check = true;
                break;
            }
            else continue;
        }
        var attGrp = new AttributeGroup{
            Name = attributeGroup.Name,
            Effect = attributeGroup.Effect
        };
        if (check == false)
        {
            await _attributeRepo.CreateAsync(attGrp);
        }
    }
    public async Task Update(Guid attributeGroupid, AttributeGroup attributeGroup)
    {

    }
    public async Task Delete(Guid attributeGroupid)
    {

    }
}
