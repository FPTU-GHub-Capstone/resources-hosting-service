using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterServices
{
    Task<ICollection<CharacterEntity>> List();
    Task<CharacterEntity> GetById(Guid characterId);
    Task<ICollection<CharacterEntity>> GetByUserId(Guid id);
    Task<ICollection<CharacterEntity>> GetByCharacterTypeId(Guid id);
    Task<ICollection<CharacterEntity>> GetByGameServerId(Guid id);
    Task<int> Count();
    Task Create(CharacterEntity character);
    Task Update(Guid characterId, CharacterEntity character);
    Task Delete(Guid characterId);
}
