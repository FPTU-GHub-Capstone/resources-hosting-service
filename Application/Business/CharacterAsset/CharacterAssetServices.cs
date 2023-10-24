using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterAssetServices : ICharacterAssetServices
{
    public readonly IGenericRepository<CharacterAssetEntity> _characterAssetRepo;

    public CharacterAssetServices(IGenericRepository<CharacterAssetEntity> characterAssetRepo)
    {
        _characterAssetRepo = characterAssetRepo;
    }
    public async Task<ICollection<CharacterAssetEntity>> List()
    {
        return await _characterAssetRepo.ListAsync();
    }
    public async Task<CharacterAssetEntity> GetById(Guid characterAssetId)
    {
        return await _characterAssetRepo.FindByIdAsync(characterAssetId);
    }
    public async Task<ICollection<CharacterAssetEntity>> GetByAssetId(Guid id)
    {
        return await _characterAssetRepo.WhereAsync(cA => cA.AssetsId.Equals(id));
    }
    public async Task<ICollection<CharacterAssetEntity>> GetByCharacterId(Guid id)
    {
        return await _characterAssetRepo.WhereAsync(cA => cA.CharacterId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _characterAssetRepo.CountAsync();
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
            throw new BadRequestException(Constants.ENTITY.CHARACTER_ASSET + Constants.ERROR.ALREADY_EXIST_ERROR);
        }
    }
}