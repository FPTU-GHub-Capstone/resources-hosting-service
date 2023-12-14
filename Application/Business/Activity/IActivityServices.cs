using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IActivityServices
{
    Task<ICollection<ActivityEntity>> List();
    Task<ActivityEntity> Search(Guid activityId);
    Task<ICollection<ActivityEntity>> ListActivitiesByGameId(Guid gameId);
    Task Create(ActivityEntity activity);
    Task Update(ActivityEntity activity);
    Task Delete(Guid activityId);
}
