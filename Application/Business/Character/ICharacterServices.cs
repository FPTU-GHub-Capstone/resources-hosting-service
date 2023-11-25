using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterServices
{
    Task<ICollection<CharacterEntity>> List();
    Task<CharacterEntity> GetById(Guid characterId);
    Task<ICollection<CharacterEntity>> ListCharByUserId(Guid id); 
    Task<ICollection<CharacterEntity>> ListCharByCharTypeId(Guid id);
    Task<ICollection<CharacterEntity>> ListCharByGameServerId(Guid id);
    Task<int> Count();
    Task Create(CharacterEntity character);
    Task Update(CharacterEntity character);
    Task Delete(Guid characterId);
}
