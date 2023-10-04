using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Asset;
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
        return await _levelProgressRepo.ListAsync();
    }
    public async Task<LevelProgress> GetById(Guid levelProgressId)
    {
        return await _levelProgressRepo.FindByIdAsync(levelProgressId);
    }
    public async Task<ICollection<LevelProgress>> GetByCharacterId(Guid id)
    {
        return await _levelProgressRepo.WhereAsync(lp => lp.CharacterId.Equals(id));
    }
    public async Task<ICollection<LevelProgress>> GetByLevelId(Guid id)
    {
        return await _levelProgressRepo.WhereAsync(lp => lp.LevelId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _levelProgressRepo.CountAsync();
    }
    public async Task Create(LevelProgress levelProgress) { }
    public async Task Update(Guid levelProgressId, LevelProgress levelProgress) { }
    public async Task Delete(Guid levelProgressId) { }
}
