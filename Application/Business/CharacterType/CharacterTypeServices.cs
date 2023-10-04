using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business.CharacterType;

public class CharacterTypeServices : ICharacterTypeServices
{
    public readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;

    public CharacterTypeServices(IGenericRepository<CharacterTypeEntity> characterTypeRepo)
    {
        _characterTypeRepo = characterTypeRepo;
    }
    public async Task<ICollection<CharacterTypeEntity>> List()
    {
        return await _characterTypeRepo.ListAsync();
    }
    public async Task<CharacterTypeEntity> GetById(Guid characterTypeId)
    {
        return await _characterTypeRepo.FindByIdAsync(characterTypeId);
    }
    public async Task<ICollection<CharacterTypeEntity>> GetByGameId(Guid gameId)
    {
        return await _characterTypeRepo.WhereAsync(c => c.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _characterTypeRepo.CountAsync();
    }
    public async Task Create(CharacterTypeEntity characterType)
    {

    }
    public async Task Update(Guid characterTypeId, CharacterTypeEntity characterType)
    {

    }
    public async Task Delete(Guid characterTypeId)
    {

    }


}
