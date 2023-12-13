using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;
using DomainLayer.Exceptions;

namespace WebApiLayer.Controllers;

[Authorize]
[Route(Constants.Http.API_VERSION + "/gms/activities")]
public class ActivitiesController : BaseController
{
    private readonly IActivityServices _activityServices;
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    public ActivitiesController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo)
    {
        _activityServices = activityServices;
        _activityRepo = activityRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivitíes()
    {
        if (!CurrentScp.Contains("activities:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _activityServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return Ok(await _activityServices.Search(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] UpdateActivityRequest act)
    {
        var updateAct = await _activityRepo.FoundOrThrowAsync(id, Constants.Entities.ACTIVITY + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(act, updateAct);
        await _activityServices.Update(updateAct);
        return Ok(updateAct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        await _activityRepo.FoundOrThrowAsync(id, Constants.Entities.ACTIVITY + Constants.Errors.NOT_EXIST_ERROR);
        await _activityServices.Delete(id);
        return NoContent();
    }
}