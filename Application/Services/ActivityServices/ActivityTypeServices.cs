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
        var activityTypes = await _activityTypeRepo.ListAsync();
        return activityTypes;
    }
    public async Task<ActivityType> GetById(Guid activityTypeId)
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

    //public async Task<ICollection<ActivityType>> GetById(Guid id, int typeId)
    //{
    //    ICollection<ActivityType> activityTypes = new Collection<ActivityType>();
    //    if(typeId == 1) // typeId: 1: GameId, 2: CharacterId
    //    {
    //        activityTypes = await _activityTypeRepo.WhereAsync(
    //            a=> a.GameId.Equals(id));
    //    }
    //    else if(typeId == 2)
    //    {
    //        activityTypes = await _activityTypeRepo.WhereAsync(
    //            a => a.CharacterId.Equals(id));
    //    }
    //    //Check if exist
    //    if (activityTypes == null || activityTypes.Count == 0)
    //    {
    //        throw new Exception($"Activity type or game Id/character Id not found");
    //    }
    //    else
    //    {
    //        return activityTypes;
    //    }
    //}
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
