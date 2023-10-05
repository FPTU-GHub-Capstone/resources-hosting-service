using DomainLayer.Entities;
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

    }
    public async Task Update(Guid characterAssetId, CharacterAssetEntity characterAsset)
    {

    }
    public async Task Delete(Guid characterAssetId)
    {

    }
}
