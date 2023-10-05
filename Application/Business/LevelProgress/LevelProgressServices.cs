using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class LevelProgressServices : ILevelProgressServices
{
    public readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    public LevelProgressServices(IGenericRepository<LevelProgressEntity> levelProgressRepo)
    {
        _levelProgressRepo = levelProgressRepo;
    }
    public async Task<ICollection<LevelProgressEntity>> List()
    {
        return await _levelProgressRepo.ListAsync();
    }
    public async Task<LevelProgressEntity> GetById(Guid levelProgressId)
    {
        return await _levelProgressRepo.FindByIdAsync(levelProgressId);
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
    public async Task Create(LevelProgressEntity levelProgress) { }
    public async Task Update(Guid levelProgressId, LevelProgressEntity levelProgress) { }
    public async Task Delete(Guid levelProgressId) { }
}
