using Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IActivityServices
{
    //Activity
    Task<ICollection<ActivityEntity>> GetActivities();
    Task<ActivityEntity> GetActivity(Guid activityId); // Get by Activity Id
    Task<ActivityEntity> GetActivity(string Name); // Get by Activity Name
    Task<ICollection<ActivityEntity>> GetActivities(Guid activityTypeId); // Get by Activity Type Id
    Task<int> CountActivities();
    Task CreateActivity(ActivityEntity activity);
    Task UpdateActivity(Guid activityId, ActivityEntity activity);
    Task DeleteActivity(Guid activityId);
    //Activity Type
    Task<ICollection<ActivityType>> GetActivityTypes();
    Task<ActivityType> GetActivityType(Guid activityTypeId); // Get by Activity Type Id
    Task<ICollection<ActivityType>> GetActivityTypes(Guid id, int typeId); // typeId: 1: GameId, 2: CharacterId
    Task<int> CountActivityTypes();
    Task CreateActivityType(ActivityType activityType);
    Task UpdateActivityType(Guid activityTypeId, ActivityType activityType);
    Task DeleteActivityType(Guid activityTypeId);
}
