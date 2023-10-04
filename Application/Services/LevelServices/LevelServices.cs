using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Asset;
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
    { // Count levels in 1 game
        var levels = await _levelRepo.WhereAsync(l => l.GameId.Equals(gameId));
        return levels.Count();
    }
    public async Task Create(LevelEntity level)
    {
       
    }
    public async Task Update(Guid levelId, LevelEntity level) { }
    public async Task Delete(Guid levelId) { }
}
