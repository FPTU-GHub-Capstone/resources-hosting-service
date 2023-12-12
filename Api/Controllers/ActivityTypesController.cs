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
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    public ActivityTypesController(IActivityTypeServices activityTypeServices, IGenericRepository<ActivityTypeEntity> activityTypeRepo
        , IGenericRepository<GameEntity> gameRepo, IGenericRepository<CharacterEntity> characterRepo)
    {
        _activityTypeServices = activityTypeServices;
        _activityTypeRepo = activityTypeRepo;
        _gameRepo = gameRepo;
        _characterRepo = characterRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivityTypes()
    {
        if (CurrentScp.Contains("activitytypes:*:get"))
        {
            return Ok(await _activityTypeServices.List());
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityType(Guid id)
    {
        return Ok(await _activityTypeServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivityType([FromBody] CreateActivityTypeRequest activityType)
    {
        await _gameRepo.FoundOrThrowAsync(activityType.GameId, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(activityType.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newActivityType = new ActivityTypeEntity();
        Mapper.Map(activityType, newActivityType);
        await _activityTypeServices.Create(newActivityType);
        return CreatedAtAction(nameof(GetActivityType), new { id = newActivityType.Id }, newActivityType);
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