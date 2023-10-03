using Application.Interfaces;
using Domain.Entities.Character;
using System;
using System.Collections.ObjectModel;

namespace Application.Services.CharacterServices;

public class CharacterAssetServices : ICharacterAssetServices
{
    public readonly IGenericRepository<CharacterAsset> _characterAssetRepo;

    public CharacterAssetServices(IGenericRepository<CharacterAsset> characterAssetRepo)
    {
        _characterAssetRepo = characterAssetRepo;
    }
    public async Task<ICollection<CharacterAsset>> List()
    {
        var charAss = await _characterAssetRepo.ListAsync();
        return charAss;
    }
    public async Task<CharacterAsset> GetById(Guid characterAssetId)
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
    public async Task<ICollection<CharacterAsset>> GetById(Guid id, int typeId)
    { // Type ID: 1: AssetId, 2: CharacterId
        ICollection<CharacterAsset> charAss = new Collection<CharacterAsset>();
        if (typeId == 1)
        {
            charAss = await _characterAssetRepo.WhereAsync(
                cA => cA.AssetsId.Equals(id));
        }
        else if (typeId == 2)
        {
            charAss = await _characterAssetRepo.WhereAsync(
                cA => cA.CharacterId.Equals(id));
        }
        if (charAss.Count == 0)
        {
            throw new Exception($"Character Asset or Asset Id / CharacterId not found");
        }
        else
        {
            return charAss;
        }
    }
    public async Task<int> Count()
    {
        return await _characterAssetRepo.CountAsync();
    }
    public async Task Create(CharacterAsset characterAsset)
    {

    }
    public async Task Update(Guid characterAssetId, CharacterAsset characterAsset)
    {

    }
    public async Task Delete(Guid characterAssetId)
    {

    }
}
