using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class LevelProgressServices : ILevelProgressServices
{
    private readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    private readonly IGenericRepository<LevelEntity> _levelRepo;
    public LevelProgressServices(IGenericRepository<LevelProgressEntity> levelProgressRepo, IGenericRepository<LevelEntity> levelRepo)
    {
        _levelProgressRepo = levelProgressRepo;
        _levelRepo = levelRepo;
    }
    public async Task<ICollection<LevelProgressEntity>> List()
    {
        return await _levelProgressRepo.ListAsync();
    }
    public async Task<LevelProgressEntity> GetById(Guid levelProgressId)
    {
        return await _levelProgressRepo.FoundOrThrowAsync(levelProgressId,
            Constants.ENTITY.LEVEL_PROGRESS + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<LevelProgressEntity>> GetByCharacterId(Guid id)
    {
        return await _levelProgressRepo.WhereAsync(lp => lp.CharacterId.Equals(id));
    }
    public async Task<ICollection<LevelProgressEntity>> GetByLevelId(Guid id)
    {
        return await _levelProgressRepo.WhereAsync(lp => lp.LevelId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _levelProgressRepo.CountAsync();
    }
    public async Task Create(LevelProgressEntity levelProgress)
    {
        await CheckDuplicateLevelProgress(levelProgress);
        await CheckLevelProgressExpPoint(levelProgress);
        await _levelProgressRepo.CreateAsync(levelProgress);
    }
    public async Task Update(LevelProgressEntity levelProgress)
    {
        await CheckLevelProgressExpPoint(levelProgress);
        await _levelProgressRepo.UpdateAsync(levelProgress);
    }
    public async Task Delete(Guid levelProgressId)
    {
        await _levelProgressRepo.DeleteSoftAsync(levelProgressId);
    }
    public async Task CheckDuplicateLevelProgress(LevelProgressEntity levelProg)
    {
        var checkLevelProg = await _levelProgressRepo.FirstOrDefaultAsync(
            lP => lP.CharacterId == levelProg.CharacterId && lP.LevelId == levelProg.LevelId);
        if (checkLevelProg is not null && (checkLevelProg.Id == Guid.Empty || checkLevelProg.Id != levelProg.Id))
        {
            throw new BadRequestException(Constants.ENTITY.LEVEL_PROGRESS + Constants.ERROR.ALREADY_EXIST_ERROR);
        }
    }
    public async Task CheckLevelProgressExpPoint(LevelProgressEntity levelProg)
    {
        var level = await _levelRepo.FoundOrThrowAsync(levelProg.LevelId);
        if (levelProg.ExpPoint > level.LevelUpPoint)
        {
            throw new BadRequestException("Exp point in level progress cannot exceed level up point in level");
        }
    }
}