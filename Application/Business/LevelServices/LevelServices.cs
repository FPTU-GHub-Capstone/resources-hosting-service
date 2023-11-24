using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Security.Cryptography;

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
        return await _levelRepo.FoundOrThrowAsync(levelId,
            Constants.ENTITY.LEVEL + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<LevelEntity>> List(Guid[] levelIds)
    {
        var levels = await _levelRepo.WhereAsync(level => levelIds.Contains(level.Id));
        return levels;
    }
    public async Task<ICollection<LevelEntity>> ListLevelsByGameId(Guid gameId)
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
    public async Task Create(List<LevelEntity> level)
    {
        await _levelRepo.CreateRangeAsync(level);
    }
    public async Task Update(LevelEntity level)
    {
        await CheckForDuplicateLevel(level.Name, level.GameId, level.Id);
        await _levelRepo.UpdateAsync(level);
    }
    public async Task Delete(Guid levelId)
    {
        await _levelRepo.DeleteSoftAsync(levelId);
    }
    public async Task CheckForDuplicateLevel(string name, Guid GameId, Guid? id = null)
    {
        var levelCheck = await _levelRepo.FirstOrDefaultAsync(l => l.Name == name && l.GameId == GameId);
        if (levelCheck is not null)
        {
            if (!id.HasValue || levelCheck.Id != id)
            {
                throw new BadRequestException(Constants.ENTITY.LEVEL + Constants.ERROR.ALREADY_EXIST_ERROR);
            }
        }
    }
}