using Application.Interfaces;
using Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ActivityServices;

public class ActivityServices : IActivityServices
{
    public readonly IGenericRepository<ActivityEntity> _activityRepo;
    public readonly IGenericRepository<ActivityType> _activityTypeRepo;
    
    public ActivityServices(IGenericRepository<ActivityEntity> activityRepo, IGenericRepository<ActivityType> activityTypeRepo)
    {
        _activityRepo = activityRepo;
        _activityTypeRepo = activityTypeRepo;
    }

    public async Task<ICollection<ActivityEntity>> GetActivities()
    {
        return null;
        //Insert function here
    }
    public async Task<ActivityEntity> GetActivity(Guid Id)
    {
        return null;
        //Insert function here
    }
    public async Task<ActivityEntity> GetActivity(string Name)
    {
        return null;
        //Insert function here
    }
    public async Task<ICollection<ActivityEntity>> GetActivities(Guid Id)
    {
        return null;
        //Insert function here
    }
    public async Task<int> CountActivities()
    {
        return 0;
        //Insert function here
    }
    public async Task CreateActivity(ActivityEntity activity)
    {
        //Insert function here
    }
    public async Task<ActivityEntity> UpdateActivity(ActivityEntity activity)
    {
        return null;
        //Insert function here
    }
    public async Task DeleteActivity(ActivityEntity activity)
    {
        //Insert function here
    }
    public async Task<ICollection<ActivityType>> GetActivityTypes()
    {
        return null;
        //Insert function here
    }
}
