using Domain.Entities.Activity;

namespace Application.Interfaces.Activity;

public interface IActivityTypeServices
{
    Task<ICollection<ActivityType>> List();
    Task<ActivityType> GetById(Guid activityTypeId);
    Task<ICollection<ActivityType>> GetByGameId(Guid id);
    Task<ICollection<ActivityType>> GetByCharacterId(Guid id);
    Task<int> Count();
    Task Create(ActivityType activityType);
    Task Update(Guid activityTypeId, ActivityType activityType);
    Task Delete(Guid activityTypeId);
}
