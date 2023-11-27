using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterTypeServices
{
    Task<ICollection<CharacterTypeEntity>> List();
    Task<CharacterTypeEntity> GetById(Guid characterTypeId);
    Task<ICollection<CharacterTypeEntity>> ListCharTypesByGameId(Guid gameId);
    Task Create(CharacterTypeEntity characterType);
    Task Update(CharacterTypeEntity characterType);
    Task Delete(Guid characterTypeId);
}
