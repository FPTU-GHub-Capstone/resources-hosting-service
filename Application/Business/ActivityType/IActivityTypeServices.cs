using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IActivityTypeServices
{
    Task<ICollection<ActivityTypeEntity>> List();
    Task<ActivityTypeEntity> GetById(Guid activityTypeId);
    Task<ICollection<ActivityTypeEntity>> ListActTypesByGameId(Guid id);
    Task<ICollection<ActivityTypeEntity>> ListActTypesByCharId(Guid id);
    Task<int> Count();
    Task Create(ActivityTypeEntity activityType);
    Task Update(ActivityTypeEntity activityType);
    Task Delete(Guid activityTypeId);
}
