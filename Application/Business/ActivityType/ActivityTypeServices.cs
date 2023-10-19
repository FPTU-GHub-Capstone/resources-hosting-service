using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class ActivityTypeServices : IActivityTypeServices
{
    public readonly IGenericRepository<ActivityTypeEntity> _activityTypeRepo;

    public ActivityTypeServices(IGenericRepository<ActivityTypeEntity> activityTypeRepo)
    {
        _activityTypeRepo = activityTypeRepo;
    }
    public async Task<ICollection<ActivityTypeEntity>> List()
    {
        return await _activityTypeRepo.ListAsync();
    }
    public async Task<ActivityTypeEntity> GetById(Guid activityTypeId)
    {
        return await _activityTypeRepo.FindByIdAsync(activityTypeId);
    }
    public async Task<ICollection<ActivityTypeEntity>> GetByGameId(Guid gameid)
    {
        return await _activityTypeRepo.WhereAsync(a => a.GameId.Equals(gameid));
    }
    public async Task<ICollection<ActivityTypeEntity>> GetByCharacterId(Guid characterid)
    {
        return await _activityTypeRepo.WhereAsync(a => a.CharacterId.Equals(characterid));
    }
    public async Task<int> Count()
    {
        return await _activityTypeRepo.CountAsync();
    }
    public async Task Create(ActivityTypeEntity activityType)
    {
        await _activityTypeRepo.CreateAsync(activityType);
    }
    public async Task Update(ActivityTypeEntity activityType)
    {
        await _activityTypeRepo.UpdateAsync(activityType);
    }
    public async Task Delete(Guid activityTypeId)
    {
        await _activityTypeRepo.DeleteSoftAsync(activityTypeId);
    }
}
