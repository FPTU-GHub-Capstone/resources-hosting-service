using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/activity-types")]
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
        if (!CurrentScp.Contains("activitytypes:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _activityTypeServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityType(Guid id)
    {
        return Ok(await _activityTypeServices.GetById(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivityType(Guid id, [FromBody] UpdateActivityTypeRequest activityType)
    {
        var updateActivityType = await _activityTypeRepo.FoundOrThrowAsync(id, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(activityType, updateActivityType);
        await _activityTypeServices.Update(updateActivityType);
        return Ok(updateActivityType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivityType(Guid id)
    {
        await _activityTypeRepo.FoundOrThrowAsync(id, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _activityTypeServices.Delete(id);
        return NoContent();
    }
}