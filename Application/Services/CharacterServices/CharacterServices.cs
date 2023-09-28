using Application.Interfaces;
using Domain.Entities.Character;
using System;
using System.Collections.ObjectModel;

namespace Application.Services.CharacterServices;

public class CharacterServices : ICharacterServices
{
    public readonly IGenericRepository<CharacterAsset> _characterAssetRepo;
    public readonly IGenericRepository<CharacterAttribute> _characterAttributeRepo;
    public readonly IGenericRepository<CharacterEntity> _characterRepo;
    public readonly IGenericRepository<CharacterType> _characterTypeRepo;

    public CharacterServices(IGenericRepository<CharacterAsset> characterAssetRepo
        , IGenericRepository<CharacterAttribute> characterAttributeRepo
        , IGenericRepository<CharacterEntity> characterRepo
        , IGenericRepository<CharacterType> characterTypeRepo)
    {
        _characterAssetRepo = characterAssetRepo;
        _characterAttributeRepo = characterAttributeRepo;
        _characterRepo = characterRepo;
        _characterTypeRepo = characterTypeRepo;
    }

    //Character Asset
    public async Task<ICollection<CharacterAsset>> GetCharacterAssets()
    {
        var charAss = await _characterAssetRepo.ListAsync();
        return charAss;
    }
    public async Task<CharacterAsset> GetCharacterAsset(Guid characterAssetId)
    {
        var charAss = await _characterAssetRepo.FindByIdAsync(characterAssetId);
        if (charAss == null)
        {
            throw new Exception($"Character asset not exist");
        }
        else
        {
            return charAss;
        }
    }
    public async Task<ICollection<CharacterAsset>> GetCharacterAssets(Guid id, int typeId) { // Type ID: 1: AssetId, 2: CharacterId
        ICollection<CharacterAsset> charAss = new Collection<CharacterAsset>();
        if(typeId == 1)
        {
            charAss = await _characterAssetRepo.WhereAsync(
                cA => cA.AssetsId.Equals(id));
        } else if (typeId == 2)
        {
            charAss = await _characterAssetRepo.WhereAsync(
                cA => cA.CharacterId.Equals(id));
        }
        if(charAss.Count == 0)
        {
            throw new Exception($"Character Asset or Asset Id / CharacterId not found");
        }
        else
        {
            return charAss;
        }
    }
    public async Task<int> CountCharacterAssets()
    {
        var charAss = await _characterAssetRepo.ListAsync();
        return charAss.Count;
    }
    public async Task CreateCharacterAsset(CharacterAsset characterAsset)
    {

    }
    public async Task UpdateCharacterAsset(Guid characterAssetId, CharacterAsset characterAsset)
    {

    }
    public async Task DeleteCharacterAsset(Guid characterAssetId)
    {

    }
    //Character Attribute
    public async Task<ICollection<CharacterAttribute>> GetCharacterAttributes()
    {
        var charAtt = await _characterAttributeRepo.ListAsync();
        return charAtt;
    }
    public async Task<CharacterAttribute> GetCharacterAttribute(Guid characterAttributeid)
    {
        var charAtt = await _characterAttributeRepo.FindByIdAsync(characterAttributeid);
        if (charAtt == null)
        {
            throw new Exception($"Character attribute not exist");
        }
        else
        {
            return charAtt;
        }
    }
    public async Task<ICollection<CharacterAttribute>> GetCharacterAttributes(Guid id, int typeId) { // TypeId: 1: CharacterId, 2: AttributeGroupId
        ICollection<CharacterAttribute> charAtt = new Collection<CharacterAttribute>();
        if(typeId == 1)
        {
            charAtt = await _characterAttributeRepo.WhereAsync(
                cA => cA.CharacterId.Equals(id));
        } else if(typeId == 2)
        {
            charAtt = await _characterAttributeRepo.WhereAsync(
                cA => cA.AttributeGroupId.Equals(id));
        }
        //Return if exist
        if(charAtt.Count == 0)
        {
            throw new Exception($"Character Attribute or Character Id/ Attribute Group Id not found");
        }
        else
        {
            return charAtt;
        }
    }
    public async Task<int> CountCharacterAttributes()
    {
        var charAtt = await _characterAttributeRepo.ListAsync();
        return charAtt.Count;
    }
    public async Task CreateCharacterAttribute(CharacterAttribute characterAttribute)
    {
        
    }
    public async Task UpdateCharacterAttribute(Guid characterAttributeid, CharacterAttribute characterAttribute)
    {

    }
    public async Task DeleteCharacterAttribute(Guid characterAttributeid)
    {

    }
    //Character
    public async Task<ICollection<CharacterEntity>> GetCharacters()
    {
        var cha = await _characterRepo.ListAsync();
        return cha;
    }
    public async Task<CharacterEntity> GetCharacter(Guid characterId)
    {
        var cha = await _characterRepo.FindByIdAsync(characterId);
        if (cha == null)
        {
            throw new Exception($"Character not exist");
        }
        else
        {
            return cha;
        }
    }
    public async Task<ICollection<CharacterEntity>> GetCharacters(Guid id, int typeId) { // TypeId: 1: UserId, 2: CharacterTypeId, 3: GameServerId
        ICollection<CharacterEntity> cha = new Collection<CharacterEntity>();
        if (typeId == 1)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.UserId.Equals(id));
        }
        else if (typeId == 2)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.CharacterTypeId.Equals(id));
        }
        else if (typeId == 3)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.GameServerId.Equals(id));
        }
        //Return if exist
        if (cha.Count == 0)
        {
            throw new Exception($"Character or User/ Character Type/ Game Server not found");
        }
        else
        {
            return cha;
        }
    }
    public async Task<int> CountCharacters()
    {
        var cha = await _characterRepo.ListAsync();
        return cha.Count;
    }
    public async Task CreateCharacter(CharacterEntity character)
    {
    }
    public async Task UpdateCharacter(Guid characterId, CharacterEntity character)
    {

    }
    public async Task DeleteCharacter(Guid characterId)
    {

    }
    //Character Type
    public async Task<ICollection<CharacterType>> GetCharacterTypes()
    {
        var chaTy = await _characterTypeRepo.ListAsync();
        return chaTy;
    }
    public async Task<CharacterType> GetCharacterType(Guid characterTypeId)
    {
        var chaTy = await _characterTypeRepo.FindByIdAsync(characterTypeId);
        if (chaTy == null)
        {
            throw new Exception($"Asset attribute not exist");
        }
        else
        {
            return chaTy;
        }
    }
    public async Task<ICollection<CharacterType>> GetCharacterTypes(Guid gameId)
    {
        ICollection<CharacterType> cha = await _characterTypeRepo.WhereAsync(
            c => c.GameId.Equals(gameId));
        if (cha.Count == 0)
        {
            throw new Exception($"Character Type or Game not found");
        }
        else
        {
            return cha;
        }
    }
    public async Task<int> CountCharacterTypes()
    {
        var chaTy = await _characterTypeRepo.ListAsync();
        return chaTy.Count;
    }
    public async Task CreateCharacterType(CharacterType characterType)
    {
        
    }
    public async Task UpdateCharacterType(Guid characterTypeId, CharacterType characterType)
    {
        
    }
    public async Task DeleteCharacterType(Guid characterTypeId)
    {
        
    }


}
