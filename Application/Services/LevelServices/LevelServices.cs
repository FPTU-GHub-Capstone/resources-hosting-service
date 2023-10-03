using Application.Interfaces;
using Domain.Entities.Asset;
using Domain.Entities.Level;
using System.Collections.ObjectModel;

namespace Application.Services.LevelServices;

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
        var levels = await _levelRepo.ListAsync();
        return levels;
    }
    public async Task<LevelEntity> GetById(Guid levelId)
    {
        var level = await _levelRepo.FindByIdAsync(levelId);
        if (level == null)
        {
            throw new Exception($"Level not exist");
        }
        else
        {
            return level;
        }
    }
    public async Task<ICollection<LevelEntity>> GetByGameId(Guid gameId)
    {
        var levels = await _levelRepo.WhereAsync(
            l => l.GameId.Equals(gameId));
        if (levels == null || levels.Count == 0)
        {
            throw new Exception($"Level or Game not exist");
        }
        else
        {
            return levels;
        }
    }
    public async Task<int> Count()
    {
        return await _levelRepo.CountAsync();
    }
    public async Task<int> Count(Guid gameId)
    { // Count levels in 1 game
        var levels = await _levelRepo.WhereAsync(
            l => l.GameId.Equals(gameId));
        if (levels == null || levels.Count == 0)
        {
            throw new Exception($"Level or Game not exist");
        }
        else
        {
            return levels.Count;
        }
    }
    public async Task Create(LevelEntity level)
    {
       
    }
    public async Task Update(Guid levelId, LevelEntity level) { }
    public async Task Delete(Guid levelId) { }
}
