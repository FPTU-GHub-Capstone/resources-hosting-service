using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterServices : ICharacterServices
{
    public readonly IGenericRepository<CharacterEntity> _characterRepo;

    public CharacterServices(IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterRepo = characterRepo;
    }
    public async Task<ICollection<CharacterEntity>> List()
    {
        return await _characterRepo.ListAsync();
    }
    public async Task<CharacterEntity> GetById(Guid characterId)
    {
        return await _characterRepo.FindByIdAsync(characterId);
    }
    public async Task<ICollection<CharacterEntity>> GetByUserId(Guid id)
    { 
        return await _characterRepo.WhereAsync(c=>c.UserId == id);
    }
    public async Task<ICollection<CharacterEntity>> GetByCharacterTypeId(Guid id)
    {
        return await _characterRepo.WhereAsync(c => c.CharacterTypeId == id);
    }
    public async Task<ICollection<CharacterEntity>> GetByGameServerId(Guid id)
    {
        return await _characterRepo.WhereAsync(c => c.GameServerId == id);
    }
    public async Task<int> Count()
    {
        return await _characterRepo.CountAsync();
    }
    public async Task Create(CharacterEntity character)
    {
    }
    public async Task Update(Guid characterId, CharacterEntity character)
    {

    }
    public async Task Delete(Guid characterId)
    {

    }
}
