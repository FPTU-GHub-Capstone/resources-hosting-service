using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace ServiceLayer.Business;

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
    public async Task Create(AttributeGroupEntity attributeGroup){
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
            await _attributeRepo.CreateAsync(attributeGroup);
        }
        else
        {
            throw new BadRequestException("Name already exist.");
        }
    }
    public async Task Update(Guid attributeGroupid, AttributeGroupEntity attributeGroup)
    {
        var target = await GetById(attributeGroupid);
        if(target is not null)
        {
            target.Name = attributeGroup.Name;
            target.Effect = attributeGroup.Effect;
            await _attributeRepo.UpdateAsync(target);
        }
        else
        {
            throw new NotFoundException("Attribute group not exist");
        }
    }
    public async Task Delete(Guid attributeGroupid)
    {
        var target = await _attributeRepo.DeleteSoftAsync(attributeGroupid);
        if (target is null)
        {
            throw new NotFoundException("Attribute group not exist");
        }
    }
}
