using DomainLayer.Entities;

namespace ServiceLayer.Business.CharacterAsset;

public interface ICharacterAssetServices
{
    Task<ICollection<CharacterAssetEntity>> List();
    Task<CharacterAssetEntity> GetById(Guid characterAssetId);
    Task<ICollection<CharacterAssetEntity>> GetByAssetId(Guid id);
    Task<ICollection<CharacterAssetEntity>> GetByCharacterId(Guid id);
    Task<int> Count();
    Task Create(CharacterAssetEntity characterAsset);
    Task Update(Guid characterAssetId, CharacterAssetEntity characterAsset);
    Task Delete(Guid characterAssetId);
}
