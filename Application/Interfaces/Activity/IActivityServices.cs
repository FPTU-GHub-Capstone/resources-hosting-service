using Domain.Entities.Activity;

namespace Application.Interfaces.Activity;

public interface IActivityServices
{
    //Activity
    Task<ICollection<ActivityEntity>> List();
    Task<ActivityEntity> GetById(Guid activityId); // Get by Activity Id
    Task<ActivityEntity> GetByName(string Name); // Get by Activity Name
    Task<ICollection<ActivityEntity>> GetByTypeId(Guid activityTypeId); // Get by Activity Type Id
    Task<int> Count();
    Task Create(ActivityEntity activity);
    Task Update(Guid activityId, ActivityEntity activity);
    Task Delete(Guid activityId);
}
