using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterAttributeServices : ICharacterAttributeServices
{
    public readonly IGenericRepository<CharacterAttributeEntity> _characterAttributeRepo;

    public CharacterAttributeServices(IGenericRepository<CharacterAttributeEntity> characterAttributeRepo)
    {
        _characterAttributeRepo = characterAttributeRepo;
    }
    public async Task<ICollection<CharacterAttributeEntity>> List()
    {
        return await _characterAttributeRepo.ListAsync();
    }
    public async Task<CharacterAttributeEntity> GetById(Guid characterAttributeid)
    {
        return await _characterAttributeRepo.FindByIdAsync(characterAttributeid);
    }
    public async Task<ICollection<CharacterAttributeEntity>> GetByCharacterId(Guid id)
    {
        return await _characterAttributeRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
    }
    public async Task<ICollection<CharacterAttributeEntity>> GetByAttributeGroupId(Guid id)
    {
        return await _characterAttributeRepo.WhereAsync(cA => cA.AttributeGroupId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _characterAttributeRepo.CountAsync();
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
    public async Task Update(Guid characterAttributeid, CharacterAttributeEntity characterAttribute)
    {
        await _characterAttributeRepo.FoundOrThrowAsync(characterAttributeid, "Character attribute not exist.");
        await _characterAttributeRepo.UpdateAsync(characterAttribute);
    }
    public async Task Delete(Guid characterAttributeid)
    {
        await _characterAttributeRepo.FoundOrThrowAsync(characterAttributeid, "Character attribute not exist.");
        await _characterAttributeRepo.DeleteSoftAsync(characterAttributeid);
    }

}
