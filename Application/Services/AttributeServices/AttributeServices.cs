using Application.Interfaces;
using Domain.Entities;

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
        return await _attributeRepo.ListAsync();
    }
    public async Task<AttributeGroup> GetById(Guid attributeGroupid)
    {
        return await _attributeRepo.FindByIdAsync(attributeGroupid);
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
        }
        if (!check)
        {
            var attGrp = new AttributeGroup
            {
                Name = attributeGroup.Name,
                Effect = attributeGroup.Effect
            };
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
