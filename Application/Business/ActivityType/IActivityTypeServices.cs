using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IActivityTypeServices
{
    Task<ICollection<ActivityTypeEntity>> List();
    Task<ActivityTypeEntity> GetById(Guid activityTypeId);
    Task<ICollection<ActivityTypeEntity>> GetByGameId(Guid id);
    Task<ICollection<ActivityTypeEntity>> GetByCharacterId(Guid id);
    Task<int> Count();
    Task Create(ActivityTypeEntity activityType);
    Task Update(ActivityTypeEntity activityType);
    Task Delete(Guid activityTypeId);
}
