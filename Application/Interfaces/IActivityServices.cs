using Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IActivityServices
    {
        //Activity Entity
        Task<ICollection<ActivityEntity>> GetActivities();
        Task<ActivityEntity> GetActivity(Guid Id); // Get by Activity Id
        Task<ActivityEntity> GetActivity(string Name); // Get by Activity Name
        Task<ICollection<ActivityEntity>> GetActivities(Guid Id); // Get by Activity Type Id
        Task<int> CountActivities();
        Task CreateActivity(ActivityEntity activity);
        Task<ActivityEntity> UpdateActivity(ActivityEntity activity);
        Task DeleteActivity(ActivityEntity activity);
        //Activity Type
        Task<ICollection<ActivityType>> GetActivityTypes();
    }
}
