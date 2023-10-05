using DomainLayer.Entities;
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

    }
    public async Task Update(Guid characterAttributeid, CharacterAttributeEntity characterAttribute)
    {

    }
    public async Task Delete(Guid characterAttributeid)
    {

    }
}
