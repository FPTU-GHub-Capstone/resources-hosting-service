using Application.Interfaces;
using Domain.Entities.Character;
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
        var charAtt = await _characterAttributeRepo.ListAsync();
        return charAtt;
    }
    public async Task<CharacterAttribute> GetById(Guid characterAttributeid)
    {
        var charAtt = await _characterAttributeRepo.FindByIdAsync(characterAttributeid);
        if (charAtt == null)
        {
            throw new Exception($"Character attribute not exist");
        }
        else
        {
            return charAtt;
        }
    }
    public async Task<ICollection<CharacterAttribute>> GetById(Guid id, int typeId) { // TypeId: 1: CharacterId, 2: AttributeGroupId
        ICollection<CharacterAttribute> charAtt = new Collection<CharacterAttribute>();
        if(typeId == 1)
        {
            charAtt = await _characterAttributeRepo.WhereAsync(
                cA => cA.CharacterId.Equals(id));
        } else if(typeId == 2)
        {
            charAtt = await _characterAttributeRepo.WhereAsync(
                cA => cA.AttributeGroupId.Equals(id));
        }
        //Return if exist
        if(charAtt.Count == 0)
        {
            throw new Exception($"Character Attribute or Character Id/ Attribute Group Id not found");
        }
        else
        {
            return charAtt;
        }
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
