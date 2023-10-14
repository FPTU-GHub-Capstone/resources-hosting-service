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
    //Level
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
        //2 levels of a same game cannot have the same name:
        if(await checkExistLevel(level) is not null)
        {
            throw new BadRequestException("The game already has this level's name");
        }
        await _levelRepo.CreateAsync(level);
    }
    public async Task Update(Guid levelId, LevelEntity level) {
        var lCheckId = await _levelRepo.FindByIdAsync(levelId);
        if (lCheckId is null)
        {
            throw new BadRequestException("Level not exist");
        }
        var lCheck = await checkExistLevel(level);
        if (lCheck is not null && !lCheck.Id.Equals(level.Id))
        {
            throw new BadRequestException("The game already has this level's name");
        }
        await _levelRepo.UpdateAsync(level);
    }
    public async Task Delete(Guid levelId) {
        var level = await _levelRepo.FindByIdAsync(levelId);
        if(level is null)
        {
            throw new BadRequestException("Level not exist");
        }
        await _levelRepo.DeleteSoftAsync(level);
    }

    public async Task<LevelEntity> checkExistLevel(LevelEntity level)
    {
        var levelCheck = await _levelRepo.FirstOrDefaultAsync(l => l.Name == level.Name && l.GameId == level.GameId);
        if (levelCheck is not null)
        {
            return levelCheck;
        }
        return null;
    }
}
