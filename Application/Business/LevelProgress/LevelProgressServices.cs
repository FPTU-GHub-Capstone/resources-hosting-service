using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace ServiceLayer.Business;

public class LevelProgressServices : ILevelProgressServices
{
    private readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    private readonly IGenericRepository<LevelEntity> _levelRepo;
    private readonly ILevelServices _levelServices;
    private readonly ICharacterServices _characterServices;
    public LevelProgressServices(IGenericRepository<LevelProgressEntity> levelProgressRepo, IGenericRepository<LevelEntity> levelRepo
        , ILevelServices levelServices, ICharacterServices characterServices)
    {
        _levelProgressRepo = levelProgressRepo;
        _levelRepo = levelRepo;
        _levelServices = levelServices;
        _characterServices = characterServices;
    }
    public async Task<ICollection<LevelProgressEntity>> List()
    {
        return await _levelProgressRepo.ListAsync();
    }
    public async Task<LevelProgressEntity> GetById(Guid levelProgressId)
    {
        return await _levelProgressRepo.FoundOrThrowAsync(levelProgressId,
            Constants.Entities.LEVEL_PROGRESS + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<LevelProgressEntity>> ListLevelProgByCharacterId(Guid charId)
    {
        var levelProgresses =  await _levelProgressRepo.WhereAsync(lP => lP.CharacterId.Equals(charId), "Level");
        return levelProgresses;
    }

    public async Task<ICollection<LevelProgressEntity>> ListLevelProgByGameId(Guid gameId)
    {
        var levelIds = (await _levelServices.ListLevelsByGameId(gameId)).Select(x => x.Id);
        var characterIds = (await _characterServices.ListCharByGameId(gameId)).Select(x => x.Id);
        return await _levelProgressRepo.WhereAsync(x => levelIds.Contains(x.LevelId) || characterIds.Contains(x.CharacterId));
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
            throw new BadRequestException(Constants.Entities.LEVEL_PROGRESS + Constants.Errors.ALREADY_EXIST_ERROR);
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