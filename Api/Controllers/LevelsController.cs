using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/levels")]
public class LevelsController : BaseController
{
    private readonly ILevelServices _levelServices;
    private readonly IGenericRepository<LevelEntity> _levelRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public LevelsController(ILevelServices levelServices, IGenericRepository<LevelEntity> levelRepo
        , IGenericRepository<GameEntity> gameRepo)
    {
        _levelServices = levelServices;
        _levelRepo = levelRepo;
        _gameRepo = gameRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetLevels()
    {
        RequiredScope("levels:*:get");
        return Ok(await _levelServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLevel(Guid id)
    {
        var level = await _levelServices.GetById(id);
        return Ok(level);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLevel(Guid id, [FromBody] UpdateLevelsRequest level)
    {
        var updateLevel = await _levelRepo.FoundOrThrowAsync(id, Constants.Entities.LEVEL + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(level, updateLevel);
        await _levelServices.Update(updateLevel);
        return Ok(updateLevel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevel(Guid id)
    {
        var level = await _levelRepo.FoundOrThrowAsync(id, Constants.Entities.LEVEL + Constants.Errors.NOT_EXIST_ERROR);
        await _levelServices.Delete(level);
        return NoContent();
    }
}