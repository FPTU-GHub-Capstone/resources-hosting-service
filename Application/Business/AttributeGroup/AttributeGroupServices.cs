using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business.AttributeServices;

public class AttributeGroupServices : IAttributeGroupServices
{
    public readonly IGenericRepository<AttributeGroupEntity> _attributeRepo;
    public AttributeGroupServices(IGenericRepository<AttributeGroupEntity> attributeRepo)
    {
        _attributeRepo = attributeRepo;
    }
    public async Task<ICollection<AttributeGroupEntity>> List()
    {
        return await _attributeRepo.ListAsync();
    }
    public async Task<AttributeGroupEntity> GetById(Guid attributeGroupid)
    {
        return await _attributeRepo.FindByIdAsync(attributeGroupid);
    }
    public async Task<int> Count()
    {
        return await _attributeRepo.CountAsync();
    }
    public async Task Create(AttributeGroupEntity attributeGroup)
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
            var attGrp = new AttributeGroupEntity
            {
                Name = attributeGroup.Name,
                Effect = attributeGroup.Effect
            };
            await _attributeRepo.CreateAsync(attGrp);
        }
    }
    public async Task Update(Guid attributeGroupid, AttributeGroupEntity attributeGroup)
    {

    }
    public async Task Delete(Guid attributeGroupid)
    {

    }
}
