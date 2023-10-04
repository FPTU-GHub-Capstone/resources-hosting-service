using Domain.Entities;

namespace Application.Interfaces;

public interface ICharacterAttributeServices
{
    Task<ICollection<CharacterAttribute>> List();
    Task<CharacterAttribute> GetById(Guid characterAttributeid);
    Task<ICollection<CharacterAttribute>> GetByCharacterId(Guid id);
    Task<ICollection<CharacterAttribute>> GetByAttributeGroupId(Guid id);
    Task<int> Count();
    Task Create(CharacterAttribute characterAttribute);
    Task Update(Guid characterAttributeid, CharacterAttribute characterAttribute);  
    Task Delete(Guid characterAttributeid);
}
