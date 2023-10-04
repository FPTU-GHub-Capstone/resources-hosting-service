using Application.Interfaces;
using Domain.Entities;
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
        return await _characterAssetRepo.ListAsync();
    }
    public async Task<CharacterAsset> GetById(Guid characterAssetId)
    {
        return await _characterAssetRepo.FindByIdAsync(characterAssetId);
    }
    public async Task<ICollection<CharacterAsset>> GetByAssetId(Guid id)
    {
        return await _characterAssetRepo.WhereAsync(cA => cA.AssetsId.Equals(id));
    }
    public async Task<ICollection<CharacterAsset>> GetByCharacterId(Guid id)
    {
        return await _characterAssetRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
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
