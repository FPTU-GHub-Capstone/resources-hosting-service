using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class AttributeGroupServices : IAttributeGroupServices
{
    private readonly IGenericRepository<AttributeGroupEntity> _attributeRepo;
    public AttributeGroupServices(IGenericRepository<AttributeGroupEntity> attributeRepo, IGameServices gameService)
    {
        _attributeRepo = attributeRepo;
    }
    public async Task<ICollection<AttributeGroupEntity>> List()
    {
        return await _attributeRepo.ListAsync();
    }
    public async Task<AttributeGroupEntity> GetById(Guid attributeGroupid)
    {
        return await _attributeRepo.FoundOrThrowAsync(attributeGroupid, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
    }

    public async Task<ICollection<AttributeGroupEntity>> ListAttributeGroupsByGameId(Guid id)
    {
        return await _attributeRepo.WhereAsync(x => x.GameId.Equals(id));
    }
    public async Task Create(AttributeGroupEntity attributeGroup){
        await _attributeRepo.CreateAsync(attributeGroup);
    }
    public async Task Update(AttributeGroupEntity attributeGroup)
    {
        await _attributeRepo.UpdateAsync(attributeGroup);
    }
    public async Task Delete(Guid attributeGroupid)
    {
        await _attributeRepo.DeleteSoftAsync(attributeGroupid);
    }
}
