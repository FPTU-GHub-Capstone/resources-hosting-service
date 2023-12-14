using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class ActivityServices : IActivityServices
{
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    private readonly IActivityTypeServices _activityTypeServices;
    
    public ActivityServices(IGenericRepository<ActivityEntity> activityRepo, IActivityTypeServices activityTypeService)
    {
        _activityRepo = activityRepo;
        _activityTypeServices = activityTypeService;
    }
    public async Task<ICollection<ActivityEntity>> List() {
        return await _activityRepo.ListAsync();
    }
    public async Task<ActivityEntity> Search(Guid activityId)
    {
        return await _activityRepo.FoundOrThrowAsync(activityId, Constants.Entities.ACTIVITY + Constants.Errors.NOT_EXIST_ERROR);
    }

    public async Task<ICollection<ActivityEntity>> ListActivitiesByGameId(Guid gameId)
    {
        var activityTypeIds = (await _activityTypeServices.ListActTypesByGameId(gameId)).Select(x=>x.Id);
        return await _activityRepo.WhereAsync(a => activityTypeIds.Contains(a.ActivityTypeId));
    }
    public async Task Create(ActivityEntity activity)
    {
        await _activityRepo.CreateAsync(activity);
    }
    public async Task Update(ActivityEntity activity)
    {
        await _activityRepo.UpdateAsync(activity);
    }
    public async Task Delete(Guid activityId)
    {
        await _activityRepo.DeleteSoftAsync(activityId);
    }
}
