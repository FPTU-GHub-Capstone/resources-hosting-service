using Application.Interfaces;
using Domain.Entities.Activity;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Application.Services.ActivityServices;

public class ActivityServices : IActivityServices
{
    public readonly IGenericRepository<ActivityEntity> _activityRepo;
    public readonly IGenericRepository<ActivityType> _activityTypeRepo;
    
    public ActivityServices(IGenericRepository<ActivityEntity> activityRepo, IGenericRepository<ActivityType> activityTypeRepo)
    {
        _activityRepo = activityRepo;
        _activityTypeRepo = activityTypeRepo;
    }
    public async Task<ICollection<ActivityEntity>> GetActivities() {
        var activities = await _activityRepo.ListAsync();
        return activities;
    }
    public async Task<ActivityEntity> GetActivity(Guid activityId)
    {
        // Get by Activity Id
        var activity = await _activityRepo.FindByIdAsync(activityId);
        if(activity == null)
        {
            throw new Exception($"Activity not found");
        }
        else
        {
            return activity;
        }
    }
    public async Task<ActivityEntity> GetActivity(string Name) {
        // Get by Activity Id
        var activity = await _activityRepo.FirstOrDefaultAsync(
            a => a.Name.Equals(Name));
        if (activity == null)
        {
            throw new Exception($"Activity not found");
        }
        else
        {
            return activity;
        }
    }
    public async Task<ICollection<ActivityEntity>> GetActivities(Guid activityTypeId)
    {
        var activities = await _activityRepo.WhereAsync(
            a => a.ActivityTypeId.Equals(activityTypeId));
        if (activities.Count == 0 || activities == null)
        {
            throw new Exception($"Activity or activity type not found");
        }
        else
        {
            return activities;
        }
    }
    public async Task<int> CountActivities()
    {
        var activities = await _activityRepo.ListAsync();
        return activities.Count;
    }
    public async Task CreateActivity(ActivityEntity activity)
    {
        
    }
    public async Task UpdateActivity(Guid activityId, ActivityEntity activity)
    {

    }
    public async Task DeleteActivity(Guid activityId)
    {

    }
    //--------------------------------------------------------------------------------
    public async Task<ICollection<ActivityType>> GetActivityTypes()
    {
        var activityTypes = await _activityTypeRepo.ListAsync();
        return activityTypes;
    }
    public async Task<ActivityType> GetActivityType(Guid activityTypeId)
    {
        // Get by Activity Type Id
        var activityType = await _activityTypeRepo.FindByIdAsync(activityTypeId);
        if (activityType == null)
        {
            throw new Exception($"Activity type not found");
        }
        else
        {
            return activityType;
        }
    }
    public async Task<ICollection<ActivityType>> GetActivityTypes(Guid id, int typeId)
    {
        ICollection<ActivityType> activityTypes = new Collection<ActivityType>();
        if(typeId == 1) // typeId: 1: GameId, 2: CharacterId
        {
            activityTypes = await _activityTypeRepo.WhereAsync(
                a=> a.GameId.Equals(id));
        }
        else if(typeId == 2)
        {
            activityTypes = await _activityTypeRepo.WhereAsync(
                a => a.CharacterId.Equals(id));
        }
        //Check if exist
        if (activityTypes == null || activityTypes.Count == 0)
        {
            throw new Exception($"Activity type or game Id/character Id not found");
        }
        else
        {
            return activityTypes;
        }
    }
    public async Task<int> CountActivityTypes()
    {
        var activityTypes = await _activityTypeRepo.ListAsync();
        return activityTypes.Count;
    }
    public async Task CreateActivityType(ActivityType activityType)
    {

    }
    public async Task UpdateActivityType(Guid activityTypeId, ActivityType activityType)
    {

    }
    public async Task DeleteActivityType(Guid activityTypeId)
    {

    }
}
