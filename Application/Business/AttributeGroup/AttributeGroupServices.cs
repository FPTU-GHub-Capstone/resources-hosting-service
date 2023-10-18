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
        await _attributeRepo.UpdateAsync(attributeGroup);
    }
    public async Task Delete(Guid attributeGroupid)
    {
        await _attributeRepo.DeleteSoftAsync(attributeGroupid);
    }
}
