using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterServices
{
    //Character
    Task<ICollection<CharacterEntity>> List();
    Task<CharacterEntity> GetById(Guid characterId);
    Task<ICollection<CharacterEntity>> GetById(Guid id, int typeId); // TypeId: 1: UserId, 2: CharacterTypeId, 3: GameServerId
    Task<int> Count();
    Task Create(CharacterEntity character);
    Task Update(Guid characterId, CharacterEntity character);
    Task Delete(Guid characterId);
}
