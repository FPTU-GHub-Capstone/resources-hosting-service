using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.ObjectModel;

namespace Application.Services.CharacterServices;

public class CharacterAttributeServices : ICharacterAttributeServices
{
    public readonly IGenericRepository<CharacterAttribute> _characterAttributeRepo;

    public CharacterAttributeServices(IGenericRepository<CharacterAttribute> characterAttributeRepo)
    {
        _characterAttributeRepo = characterAttributeRepo;
    }
    public async Task<ICollection<CharacterAttribute>> List()
    {
        return await _characterAttributeRepo.ListAsync();
    }
    public async Task<CharacterAttribute> GetById(Guid characterAttributeid)
    {
        return await _characterAttributeRepo.FindByIdAsync(characterAttributeid);
    }
    public async Task<ICollection<CharacterAttribute>> GetByCharacterId(Guid id) {
        return await _characterAttributeRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
    }
    public async Task<ICollection<CharacterAttribute>> GetByAttributeGroupId(Guid id)
    {
        return await _characterAttributeRepo.WhereAsync(cA => cA.AttributeGroupId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _characterAttributeRepo.CountAsync();
    }
    public async Task Create(CharacterAttribute characterAttribute)
    {
        
    }
    public async Task Update(Guid characterAttributeid, CharacterAttribute characterAttribute)
    {

    }
    public async Task Delete(Guid characterAttributeid)
    {

    } 
}
