using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/activities")]
public class ActivitiesController : BaseController
{
    private readonly IActivityServices _activityServices;
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    private readonly IGenericRepository<ActivityTypeEntity> _activityTypeRepo;
    private readonly IGenericRepository<TransactionEntity> _transactionRepo;
    public ActivitiesController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo
        , IGenericRepository<ActivityTypeEntity> activityTypeRepo, IGenericRepository<TransactionEntity> transactionRepo)
    {
        _activityServices = activityServices;
        _activityRepo = activityRepo;
        _activityTypeRepo = activityTypeRepo;
        _transactionRepo = transactionRepo;
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
        await _activityTypeRepo.FoundOrThrowAsync(act.ActivityTypeId, Constants.ENTITY.ACTIVITY_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        await _transactionRepo.FoundOrThrowAsync(act.TransactionId, Constants.ENTITY.TRANSACTION + Constants.ERROR.NOT_EXIST_ERROR);
        var newAct = new ActivityEntity();
        Mapper.Map(act, newAct);
        await _activityServices.Create(newAct);
        return CreatedAtAction(nameof(GetActivity), new { id = newAct.Id }, newAct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] UpdateActivityRequest act)
    {
        var updateAct = await _activityRepo.FoundOrThrowAsync(id, Constants.ENTITY.ACTIVITY + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(act, updateAct);
        await _activityServices.Update(updateAct);
        return Ok(updateAct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        await _activityRepo.FoundOrThrowAsync(id, Constants.ENTITY.ACTIVITY + Constants.ERROR.NOT_EXIST_ERROR);
        await _activityServices.Delete(id);
        return NoContent();
    }
}