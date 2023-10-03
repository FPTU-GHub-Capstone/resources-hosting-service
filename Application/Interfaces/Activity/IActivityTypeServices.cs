using Domain.Entities.Activity;

namespace Application.Interfaces.Activity;

public interface IActivityTypeServices
{
    //Activity Type
    Task<ICollection<ActivityType>> List();
    Task<ActivityType> GetById(Guid activityTypeId); // Get by Activity Type Id
    //Task<ICollection<ActivityType>> GetById(Guid id, int typeId); // typeId: 1: GameId, 2: CharacterId
    Task<int> Count();
    Task Create(ActivityType activityType);
    Task Update(Guid activityTypeId, ActivityType activityType);
    Task Delete(Guid activityTypeId);
}
