using Application.Interfaces;
using Domain.Entities.Asset;
using Domain.Entities.Level;
using System.Collections.ObjectModel;

namespace Application.Services.LevelServices;

public class LevelProgressServices : ILevelProgressServices
{
    public readonly IGenericRepository<LevelProgress> _levelProgressRepo;
    public LevelProgressServices(IGenericRepository<LevelProgress> levelProgressRepo)
    {
        _levelProgressRepo = levelProgressRepo;
    }
    public async Task<ICollection<LevelProgress>> List()
    {
        var levelProgress = await _levelProgressRepo.ListAsync();
        return levelProgress;
    }
    public async Task<LevelProgress> GetById(Guid levelProgressId)
    {
        var levelProgress = await _levelProgressRepo.FindByIdAsync(levelProgressId);
        return levelProgress;
    }
    public async Task<ICollection<LevelProgress>> GetById(Guid id, int typeId)
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
    public async Task<int> Count()
    {
        return await _levelProgressRepo.CountAsync();
    }
    public async Task Create(LevelProgress levelProgress) { }
    public async Task Update(Guid levelProgressId, LevelProgress levelProgress) { }
    public async Task Delete(Guid levelProgressId) { }
}
