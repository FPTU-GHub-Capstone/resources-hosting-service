using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Threading.Tasks;

namespace ServiceLayer.Business;

public class CharacterAttributeServices : ICharacterAttributeServices
{
    private readonly IGenericRepository<CharacterAttributeEntity> _characterAttributeRepo;
    private readonly ICharacterServices _characterServices;
    private readonly IAttributeGroupServices _attributeGroupServices;

    public CharacterAttributeServices(IGenericRepository<CharacterAttributeEntity> characterAttributeRepo, ICharacterServices characterServices
        , IAttributeGroupServices attributeGroupServices)
    {
        _characterAttributeRepo = characterAttributeRepo;
        _characterServices = characterServices;
        _attributeGroupServices = attributeGroupServices;
    }
    public async Task<ICollection<CharacterAttributeEntity>> List()
    {
        return await _characterAttributeRepo.ListAsync();
    }
    public async Task<CharacterAttributeEntity> GetById(Guid characterAttributeid)
    {
        return await _characterAttributeRepo.FoundOrThrowAsync(characterAttributeid, 
            Constants.Entities.CHARACTER_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);

    }
    public async Task<ICollection<CharacterAttributeEntity>> ListCharAttByCharId(Guid id)
    {
        return await _characterAttributeRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
    }

    public async Task<ICollection<CharacterAttributeEntity>> ListCharAttByGameId(Guid id)
    {
        var characterIds = (await _characterServices.ListCharByGameId(id)).Select(x => x.Id);
        var attributeGroupIds = (await _attributeGroupServices.ListAttributeGroupsByGameId(id)).Select(x => x.Id);
        return await _characterAttributeRepo.WhereAsync(c=>characterIds.Contains(c.CharacterId) 
            || attributeGroupIds.Contains(c.AttributeGroupId));
    }
    public async Task Create(CharacterAttributeEntity characterAttribute)
    {
        var charAttCheck = await _characterAttributeRepo.FirstOrDefaultAsync(
            cA => cA.CharacterId.Equals(characterAttribute.CharacterId) && cA.AttributeGroupId.Equals(characterAttribute.AttributeGroupId));
        if(charAttCheck is not null)
        {
            throw new BadRequestException("Character with this attribute group already exist.");
        }
        await _characterAttributeRepo.CreateAsync(characterAttribute);
    }
    public async Task Update(CharacterAttributeEntity characterAttribute)
    {
        await _characterAttributeRepo.UpdateAsync(characterAttribute);
    }
    public async Task Delete(Guid characterAttributeid)
    {
        await _characterAttributeRepo.DeleteSoftAsync(characterAttributeid);
    }

}
