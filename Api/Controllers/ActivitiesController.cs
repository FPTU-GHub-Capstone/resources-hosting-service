using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/activities")]
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
        return Ok(await _activityServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return Ok(await _activityServices.Search(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest act)
    {
        var newAct = new ActivityEntity();
        Mapper.Map(act, newAct);
        await _activityServices.Create(newAct);
        return CreatedAtAction(nameof(GetActivity), new { id = newAct.Id }, newAct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] UpdateActivityRequest act)
    {
        var updateAct = await _activityRepo.FoundOrThrowAsync(id, "Activity not exist.");
        Mapper.Map(act, updateAct);
        await _activityServices.Update(updateAct);
        return Ok(updateAct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        await _activityRepo.FoundOrThrowAsync(id, "Activity not exist.");
        await _activityServices.Delete(id);
        return NoContent();
    }
}
