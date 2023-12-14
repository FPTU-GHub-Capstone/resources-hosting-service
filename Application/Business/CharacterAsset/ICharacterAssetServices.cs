using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterAssetServices
{
    Task<ICollection<CharacterAssetEntity>> List(Guid? characterId);
    Task<CharacterAssetEntity> GetById(Guid characterAssetId);
    Task<ICollection<CharacterAssetEntity>> ListCharAssetsByCharId(Guid id);
    Task<ICollection<CharacterAssetEntity>> ListCharAssetsByGameId(Guid id);
    Task Create(CharacterAssetEntity characterAsset);
    Task Update(CharacterAssetEntity characterAsset);
    Task Delete(Guid characterAssetId);
}
