using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RepositoryLayer.Repositories;
using System.Reflection;
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
        await _attributeRepo.CreateAsync(attributeGroup);
    }
    public async Task Update(Guid attributeGroupid, AttributeGroupEntity attributeGroup)
    {
        var target = await GetById(attributeGroupid);
        if(target is not null)
        {
            await _attributeRepo.UpdateAsync(target);
        }
        else
        {
            throw new NotFoundException("Attribute group not exist");
        }
    }
    public async Task Delete(Guid attributeGroupid)
    {

        var target = await GetById(attributeGroupid);
        if (target is null)
        {
            throw new NotFoundException("Attribute group not exist");
        }
        else
        {
            await _attributeRepo.DeleteSoftAsync(attributeGroupid);
        }
    }
}
