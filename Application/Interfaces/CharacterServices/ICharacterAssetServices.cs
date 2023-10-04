using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterAssetServices
{
    Task<ICollection<CharacterAsset>> List();
    Task<CharacterAsset> GetById(Guid characterAssetId);
    Task<ICollection<CharacterAsset>> GetByAssetId(Guid id);
    Task<ICollection<CharacterAsset>> GetByCharacterId(Guid id);
    Task<int> Count();
    Task Create(CharacterAsset characterAsset);
    Task Update(Guid characterAssetId, CharacterAsset characterAsset);
    Task Delete(Guid characterAssetId);
}
