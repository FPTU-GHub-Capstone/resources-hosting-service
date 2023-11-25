using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterAttributeServices
{
    Task<ICollection<CharacterAttributeEntity>> List();
    Task<CharacterAttributeEntity> GetById(Guid characterAttributeid);
    Task<ICollection<CharacterAttributeEntity>> ListCharAttByCharId(Guid id); //ListCharAttByCharId
    Task Create(CharacterAttributeEntity characterAttribute);
    Task Update(CharacterAttributeEntity characterAttribute);
    Task Delete(Guid characterAttributeid);
}
