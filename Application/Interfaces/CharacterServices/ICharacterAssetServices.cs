using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterAssetServices
{
    //Character Asset
    Task<ICollection<CharacterAsset>> List();
    Task<CharacterAsset> GetById(Guid characterAssetId);
    Task<ICollection<CharacterAsset>> GetById(Guid id, int typeId); // Type ID: 1: AssetId, 2: CharacterId
    Task<int> Count();
    Task Create(CharacterAsset characterAsset);
    Task Update(Guid characterAssetId, CharacterAsset characterAsset);
    Task Delete(Guid characterAssetId);
}
