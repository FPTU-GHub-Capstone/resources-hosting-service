using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterServices
{
    Task<ICollection<CharacterEntity>> List();
    Task<CharacterEntity> GetById(Guid characterId);
    Task<ICollection<CharacterEntity>> ListCharByUserId(Guid id); 
    Task Create(CharacterEntity character);
    Task Update(CharacterEntity character);
    Task Delete(Guid characterId);
}
