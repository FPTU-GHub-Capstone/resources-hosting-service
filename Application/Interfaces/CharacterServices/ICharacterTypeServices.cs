using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterTypeServices
{
    Task<ICollection<CharacterType>> List();
    Task<CharacterType> GetById(Guid characterTypeId);
    Task<ICollection<CharacterType>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(CharacterType characterType);
    Task Update(Guid characterTypeId, CharacterType characterType);
    Task Delete(Guid characterTypeId);
}
