using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterTypeServices : ICharacterTypeServices
{
    public readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    public readonly IGenericRepository<GameEntity> _gameRepo;

    public CharacterTypeServices(IGenericRepository<CharacterTypeEntity> characterTypeRepo, IGenericRepository<GameEntity> gameRepo)
    {
        _characterTypeRepo = characterTypeRepo;
        _gameRepo = gameRepo;
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
        var ctCheck = await _characterTypeRepo.FirstOrDefaultAsync(
            ct => ct.Name.Equals(characterType.Name));
        if (ctCheck != null)
        {
            throw new BadRequestException("Name already exist");
        }
        await _characterTypeRepo.CreateAsync(characterType);
    }
    public async Task Update(Guid characterTypeId, CharacterTypeEntity characterType)
    {
        var ctCheck = await _characterTypeRepo.FindByIdAsync(characterTypeId);
        if (ctCheck is null)
        {
            throw new BadRequestException("Character type not exist");
        }
        await _characterTypeRepo.UpdateAsync(characterType);
    }
    public async Task Delete(Guid characterTypeId)
    {
        var ctCheck = await _characterTypeRepo.FindByIdAsync(characterTypeId);
        if (ctCheck is null)
        {
            throw new BadRequestException("Character type not exist");
        }
        await _characterTypeRepo.DeleteSoftAsync(characterTypeId);
    }


}
