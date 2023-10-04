using Application.Interfaces;
using Application.Interfaces.Activity;
using Domain.Entities.Activity;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Application.Services.ActivityServices;

public class ActivityTypeServices : IActivityTypeServices
{
    public readonly IGenericRepository<ActivityType> _activityTypeRepo;
    
    public ActivityTypeServices(IGenericRepository<ActivityType> activityTypeRepo)
    {
        _activityTypeRepo = activityTypeRepo;
    }
    public async Task<ICollection<ActivityType>> List()
    {
        return await _activityTypeRepo.ListAsync();
    }
    public async Task<ActivityType> GetById(Guid activityTypeId)
    {
        return await _activityTypeRepo.FindByIdAsync(activityTypeId);      
    }
    public async Task<ICollection<ActivityType>> GetByGameId(Guid gameid)
    {
        return await _activityTypeRepo.WhereAsync(a => a.GameId.Equals(gameid));
    }
    public async Task<ICollection<ActivityType>> GetByCharacterId(Guid characterid)
    {
        return await _activityTypeRepo.WhereAsync(a => a.CharacterId.Equals(characterid));
    }
    public async Task<int> Count()
    {
        return await _activityTypeRepo.CountAsync();
    }
    public async Task Create(ActivityType activityType)
    {

    }
    public async Task Update(Guid activityTypeId, ActivityType activityType)
    {

    }
    public async Task Delete(Guid activityTypeId)
    {

    }
}
