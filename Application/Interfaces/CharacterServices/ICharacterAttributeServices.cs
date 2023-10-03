using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterAttributeServices
{
    //Character Attribute
    Task<ICollection<CharacterAttribute>> List();
    Task<CharacterAttribute> GetById(Guid characterAttributeid);
    Task<ICollection<CharacterAttribute>> GetById(Guid id, int typeId); // TypeId: 1: CharacterId, 2: AttributeGroupId
    Task<int> Count();
    Task Create(CharacterAttribute characterAttribute);
    Task Update(Guid characterAttributeid, CharacterAttribute characterAttribute);  
    Task Delete(Guid characterAttributeid);
}
