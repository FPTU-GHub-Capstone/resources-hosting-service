using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/level-progresses")]
public class LevelProgressesController : BaseController
{
    private readonly ILevelProgressServices _levelProgressServices;
    private readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    public LevelProgressesController(ILevelProgressServices levelProgressServices, IGenericRepository<LevelProgressEntity> levelProgressRepo)
    {
        _levelProgressServices = levelProgressServices;
        _levelProgressRepo = levelProgressRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetLevelProgresses()
    {
        RequiredScope("levelprogresses:*:get");
        return Ok(await _levelProgressServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLevelProgress(Guid id)
    {
        return Ok(await _levelProgressServices.GetById(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLevelProgress(Guid id, [FromBody] UpdateLevelProgressRequest levelProg)
    {
        var updateLevelProg = await _levelProgressRepo.FoundOrThrowAsync(id, Constants.Entities.LEVEL_PROGRESS + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(levelProg, updateLevelProg);
        await _levelProgressServices.Update(updateLevelProg);
        return Ok(updateLevelProg);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevelProgress(Guid id)
    {
        await _levelProgressRepo.FoundOrThrowAsync(id, Constants.Entities.LEVEL_PROGRESS + Constants.Errors.NOT_EXIST_ERROR);
        await _levelProgressServices.Delete(id);
        return NoContent();
    }
}