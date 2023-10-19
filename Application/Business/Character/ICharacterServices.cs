using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterServices
{
    Task<ICollection<CharacterEntity>> List();
    Task<CharacterEntity> GetById(Guid characterId);
    Task<ICollection<CharacterEntity>> GetByUserId(Guid id);
    Task<ICollection<CharacterEntity>> GetByCharacterTypeId(Guid id);
    Task<ICollection<CharacterEntity>> GetByGameServerId(Guid id);
    Task<int> Count();
    Task Create(CharacterEntity character);
    Task Update(CharacterEntity character);
    Task Delete(Guid characterId);
}
