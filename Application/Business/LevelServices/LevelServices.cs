using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class LevelServices : ILevelServices
{
    public readonly IGenericRepository<LevelEntity> _levelRepo;
    public LevelServices(IGenericRepository<LevelEntity> levelRepo)
    {
        _levelRepo = levelRepo;
    }
    public async Task<ICollection<LevelEntity>> List()
    {
        return await _levelRepo.ListAsync();
    }
    public async Task<LevelEntity> GetById(Guid levelId)
    {
        return await _levelRepo.FindByIdAsync(levelId);
    }
    public async Task<ICollection<LevelEntity>> GetByGameId(Guid gameId)
    {
        return await _levelRepo.WhereAsync(l => l.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _levelRepo.CountAsync();
    }
    public async Task<int> Count(Guid gameId)
    {
        var levels = await _levelRepo.WhereAsync(l => l.GameId.Equals(gameId));
        return levels.Count();
    }
    public async Task Create(LevelEntity level)
    {
        await CheckForDuplicateLevel(level);
        await _levelRepo.CreateAsync(level);
    }
    public async Task Update(LevelEntity level) {
        await CheckForDuplicateLevel(level);
        await _levelRepo.UpdateAsync(level);
    }
    public async Task Delete(Guid levelId) {
        await _levelRepo.DeleteSoftAsync(levelId);
    }
    public async Task CheckForDuplicateLevel(LevelEntity level)
    {
        var levelCheck = await _levelRepo.FirstOrDefaultAsync(l => l.Name == level.Name && l.GameId == level.GameId);
        if (levelCheck is not null)
        {
            if(level.Id == Guid.Empty || levelCheck.Id != level.Id)
            {
                throw new BadRequestException("The user already have a character in this game server");
            }
        }
    }
}
