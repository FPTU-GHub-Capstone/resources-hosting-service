using Application.Interfaces;
using Application.Interfaces.Activity;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Application.Services.ActivityServices;

public class ActivityServices : IActivityServices
{
    public readonly IGenericRepository<ActivityEntity> _activityRepo;
    
    public ActivityServices(IGenericRepository<ActivityEntity> activityRepo)
    {
        _activityRepo = activityRepo;
    }
    public async Task<ICollection<ActivityEntity>> List() {
        return await _activityRepo.ListAsync();
    }
    public async Task<ActivityEntity> Search(Guid activityId)
    {
        return await _activityRepo.FindByIdAsync(activityId);
    }
    public async Task<ActivityEntity> Search(string Name) {
        return await _activityRepo.FirstOrDefaultAsync( a => a.Name.Equals(Name));
    }
    public async Task<ICollection<ActivityEntity>> SearchByTypeId(Guid activityTypeId)
    {
        return await _activityRepo.WhereAsync(
            a => a.ActivityTypeId.Equals(activityTypeId));
    }
    public async Task<int> Count()
    {
        return await _activityRepo.CountAsync();
    }
    public async Task Create(ActivityEntity activity)
    {
        
    }
    public async Task Update(Guid activityId, ActivityEntity activity)
    {

    }
    public async Task Delete(Guid activityId)
    {

    }
}
