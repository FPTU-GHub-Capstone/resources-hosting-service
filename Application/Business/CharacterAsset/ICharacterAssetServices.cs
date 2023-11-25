using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterAssetServices
{
    Task<ICollection<CharacterAssetEntity>> List(Guid? characterId);
    Task<CharacterAssetEntity> GetById(Guid characterAssetId);
    Task<ICollection<CharacterAssetEntity>> ListCharAssByAssId(Guid id);
    Task<ICollection<CharacterAssetEntity>> ListCharAssByCharId(Guid id);
    Task<int> Count();
    Task Create(CharacterAssetEntity characterAsset);
    Task Update(CharacterAssetEntity characterAsset);
    Task Delete(Guid characterAssetId);
}
