using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/activity-types")]
public class ActivityTypesController : BaseController
{
    private readonly IActivityTypeServices _activityTypeServices;
    private readonly IGenericRepository<ActivityTypeEntity> _activityTypeRepo;
    public ActivityTypesController(IActivityTypeServices activityTypeServices, IGenericRepository<ActivityTypeEntity> activityTypeRepo)
    {
        _activityTypeServices = activityTypeServices;
        _activityTypeRepo = activityTypeRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivityTypes()
    {
        return Ok(await _activityTypeServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityType(Guid id)
    {
        return Ok(await _activityTypeServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivityType([FromBody] CreateActivityTypeRequest activityType)
    {
        var newActivityType = new ActivityTypeEntity();
        Mapper.Map(activityType, newActivityType);
        await _activityTypeServices.Create(newActivityType);
        return CreatedAtAction("GetActivityType", new { id = newActivityType.Id }, newActivityType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivityType(Guid id, [FromBody] UpdateActivityTypeRequest activityType)
    {
        var updateActivityType = await _activityTypeRepo.FoundOrThrowAsync(id, "Activity type not exist.");
        Mapper.Map(activityType, updateActivityType);
        await _activityTypeServices.Update(updateActivityType);
        return Ok(updateActivityType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivityType(Guid id)
    {
        await _activityTypeRepo.FoundOrThrowAsync(id, "Activity type not exist.");
        await _activityTypeServices.Delete(id);
        return NoContent();
    }
}
