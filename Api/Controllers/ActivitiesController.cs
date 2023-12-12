using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Text.RegularExpressions;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Authorize]
[Route(Constants.Http.API_VERSION + "/gms/activities")]
public class ActivitiesController : BaseController
{
    private readonly IActivityServices _activityServices;
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    private readonly IGenericRepository<ActivityTypeEntity> _activityTypeRepo;
    private readonly IGenericRepository<TransactionEntity> _transactionRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    public ActivitiesController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo
        , IGenericRepository<ActivityTypeEntity> activityTypeRepo, IGenericRepository<TransactionEntity> transactionRepo,
        IGenericRepository<CharacterEntity> characterRepo)
    {
        _activityServices = activityServices;
        _activityRepo = activityRepo;
        _activityTypeRepo = activityTypeRepo;
        _transactionRepo = transactionRepo;
        _characterRepo = characterRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivitíes()
    {
        if (CurrentScp.Contains("activities:*:get"))
        {
            return Ok(await _activityServices.List());
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return Ok(await _activityServices.Search(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest act)
    {
        await _activityTypeRepo.FoundOrThrowAsync(act.ActivityTypeId, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _transactionRepo.FoundOrThrowAsync(act.TransactionId, Constants.Entities.TRANSACTION + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(act.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newAct = new ActivityEntity();
        Mapper.Map(act, newAct);
        await _activityServices.Create(newAct);
        return CreatedAtAction(nameof(GetActivity), new { id = newAct.Id }, newAct);
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