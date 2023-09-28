using Domain.Entities.Character;

namespace Application.Interfaces;

public interface ICharacterServices
{
    //Character Asset
    Task<ICollection<CharacterAsset>> GetCharacterAssets();
    Task<CharacterAsset> GetCharacterAsset(Guid characterAssetId);
    Task<ICollection<CharacterAsset>> GetCharacterAssets(Guid id, int typeId); // Type ID: 1: AssetId, 2: CharacterId
    Task<int> CountCharacterAssets();
    Task CreateCharacterAsset(CharacterAsset characterAsset);
    Task UpdateCharacterAsset(Guid characterAssetId, CharacterAsset characterAsset);
    Task DeleteCharacterAsset(Guid characterAssetId);
    //Character Attribute
    Task<ICollection<CharacterAttribute>> GetCharacterAttributes();
    Task<CharacterAttribute> GetCharacterAttribute(Guid characterAttributeid);
    Task<ICollection<CharacterAttribute>> GetCharacterAttributes(Guid id, int typeId); // TypeId: 1: CharacterId, 2: AttributeGroupId
    Task<int> CountCharacterAttributes();
    Task CreateCharacterAttribute(CharacterAttribute characterAttribute);
    Task UpdateCharacterAttribute(Guid characterAttributeid, CharacterAttribute characterAttribute);  
    Task DeleteCharacterAttribute(Guid characterAttributeid);
    //Character
    Task<ICollection<CharacterEntity>> GetCharacters();
    Task<CharacterEntity> GetCharacter(Guid characterId);
    Task<ICollection<CharacterEntity>> GetCharacters(Guid id, int typeId); // TypeId: 1: UserId, 2: CharacterTypeId, 3: GameServerId
    Task<int> CountCharacters();
    Task CreateCharacter(CharacterEntity character);
    Task UpdateCharacter(Guid characterId, CharacterEntity character);
    Task DeleteCharacter(Guid characterId);
    //Character Type
    Task<ICollection<CharacterType>> GetCharacterTypes();
    Task<CharacterType> GetCharacterType(Guid characterTypeId);
    Task<ICollection<CharacterType>> GetCharacterTypes(Guid gameId);
    Task<int> CountCharacterTypes();
    Task CreateCharacterType(CharacterType characterType);
    Task UpdateCharacterType(Guid characterTypeId, CharacterType characterType);
    Task DeleteCharacterType(Guid characterTypeId);
}
