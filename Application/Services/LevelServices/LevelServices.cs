using Application.Interfaces;
using Domain.Entities.Asset;
using Domain.Entities.Level;
using System.Collections.ObjectModel;

namespace Application.Services.LevelServices;

public class LevelServices : ILevelServices
{
    public readonly IGenericRepository<LevelEntity> _levelRepo;
    public readonly IGenericRepository<LevelProgress> _levelProgressRepo;
    public LevelServices(IGenericRepository<LevelEntity> levelRepo, IGenericRepository<LevelProgress> levelProgressRepo)
    {
        _levelRepo = levelRepo;
        _levelProgressRepo = levelProgressRepo;
    }
    //Level
    public async Task<ICollection<LevelEntity>> GetLevels()
    {
        var levels = await _levelRepo.ListAsync();
        return levels;
    }
    public async Task<LevelEntity> GetLevel(Guid levelId)
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
    public async Task<ICollection<LevelEntity>> GetLevels(Guid gameId)
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
    public async Task<int> CountLevels()
    {
        var levels = await _levelRepo.ListAsync();
        return levels.Count;
    }
    public async Task<int> CountLevels(Guid gameId)
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
    public async Task CreateLevel(LevelEntity level)
    {
       
    }
    public async Task UpdateLevel(Guid levelId, LevelEntity level) { }
    public async Task DeleteLevel(Guid levelId) { }
    //Level Progress
    public async Task<ICollection<LevelProgress>> GetLevelProgresses()
    {
        var levelProgress = await _levelProgressRepo.ListAsync();
        return levelProgress;
    }
    public async Task<LevelProgress> GetLevelProgress(Guid levelProgressId)
    {
        var levelProgress = await _levelProgressRepo.FindByIdAsync(levelProgressId);
        return levelProgress;
    }
    public async Task<ICollection<LevelProgress>> GetLevelProgresses(Guid id, int typeId)
    { // TypeID: 1: CharacterId, 2: LevelId
        ICollection<LevelProgress> levelProgresses = new Collection<LevelProgress>();
        if (typeId == 1)
        {
            levelProgresses = await _levelProgressRepo.WhereAsync(
                a => a.CharacterId.Equals(id));
        }
        else if (typeId == 2)
        {
            levelProgresses = await _levelProgressRepo.WhereAsync(
                a => a.LevelId.Equals(id));
        }
        //Return if exist
        if (levelProgresses.Count == 0 || levelProgresses == null)
        {
            throw new Exception($"Level progress or character/level not found");
        }
        else
        {
            return levelProgresses;
        }
    }
    public async Task<int> CountLevelProgresses()
    {
        var levelProgress = await _levelProgressRepo.ListAsync();
        return levelProgress.Count;
    }
    public async Task CreateLevelProgress(LevelProgress levelProgress) { }
    public async Task UpdateLevelProgress(Guid levelProgressId, LevelProgress levelProgress) { }
    public async Task DeleteLevelProgress(Guid levelProgressId) { }
}
