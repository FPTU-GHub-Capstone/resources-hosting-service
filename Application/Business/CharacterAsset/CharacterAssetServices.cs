using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterAssetServices : ICharacterAssetServices
{
    private readonly IGenericRepository<CharacterAssetEntity> _characterAssetRepo;
    private readonly ICharacterServices _characterServices;
    private readonly IAssetServices _assetServices;
   
    public CharacterAssetServices(IGenericRepository<CharacterAssetEntity> characterAssetRepo, ICharacterServices characterService
        , IAssetServices assetServices)
    {
        _characterAssetRepo = characterAssetRepo;
        _characterServices = characterService;
        _assetServices = assetServices;
    }
    public async Task<ICollection<CharacterAssetEntity>> List(Guid? characterId)
    {
        if (characterId.HasValue)
        {
            var cA = await _characterAssetRepo.WhereAsync(u => u.CharacterId.Equals(characterId));
            return cA;
        }
        return await _characterAssetRepo.ListAsync();
    }
    public async Task<CharacterAssetEntity> GetById(Guid characterAssetId)
    {
        return await _characterAssetRepo.FoundOrThrowAsync(characterAssetId, Constants.Entities.CHARACTER_ASSET + Constants.Errors.NOT_EXIST_ERROR);

    }
    public async Task<ICollection<CharacterAssetEntity>> ListCharAssetsByCharId(Guid id)
    {
        return await _characterAssetRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
    }

    public async Task<ICollection<CharacterAssetEntity>> ListCharAssetsByGameId(Guid id)
    {
        var characterIds = (await _characterServices.ListCharByGameId(id)).Select(x => x.Id);
        var assetIds = (await _assetServices.ListAssetsByGameId(id)).Select(x => x.Id);
        return await _characterAssetRepo.WhereAsync(x => characterIds.Contains(x.CharacterId) || assetIds.Contains(x.AssetsId));
    }
    public async Task Create(CharacterAssetEntity characterAsset)
    {
        await CheckDuplicateCharacterAsset(characterAsset);
        await _characterAssetRepo.CreateAsync(characterAsset);
    }
    public async Task Update(CharacterAssetEntity characterAsset)
    {
        await _characterAssetRepo.UpdateAsync(characterAsset);
    }
    public async Task Delete(Guid characterAssetId)
    {
        await _characterAssetRepo.DeleteSoftAsync(characterAssetId);
    }
    public async Task CheckDuplicateCharacterAsset(CharacterAssetEntity charAss)
    {
        var checkCharAss = await _characterAssetRepo.FirstOrDefaultAsync(
            cA => cA.AssetsId.Equals(charAss.AssetsId) && cA.CharacterId.Equals(charAss.CharacterId));
        if (checkCharAss is not null && (charAss.Id == Guid.Empty || checkCharAss.Id != charAss.Id))
        {
            throw new BadRequestException(Constants.Entities.CHARACTER_ASSET + Constants.Errors.ALREADY_EXIST_ERROR);
        }
    }
}