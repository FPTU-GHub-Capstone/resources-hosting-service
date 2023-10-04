using Domain.Entities.Activity;

namespace Application.Interfaces.Activity;

public interface IActivityServices
{
    Task<ICollection<ActivityEntity>> List();
    Task<ActivityEntity> Search(Guid activityId);
    Task<ActivityEntity> Search(string Name);
    Task<ICollection<ActivityEntity>> SearchByTypeId(Guid activityTypeId);
    Task<int> Count();
    Task Create(ActivityEntity activity);
    Task Update(Guid activityId, ActivityEntity activity);
    Task Delete(Guid activityId);
}
