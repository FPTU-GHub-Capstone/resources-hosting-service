using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/levels")]
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
        var levels = await _levelServices.List();
        return Ok(levels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLevel(Guid id)
    {
        var level = await _levelServices.GetById(id);
        return Ok(level);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLevel([FromBody] CreateLevelsController level)
    {
        await _gameRepo.FoundOrThrowAsync(level.GameId, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        var newLevel = new LevelEntity();
        Mapper.Map(level, newLevel);
        await _levelServices.Create(newLevel);
        return CreatedAtAction(nameof(GetLevel), new { id = newLevel.Id }, newLevel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLevel(Guid id, [FromBody] UpdateLevelsController level)
    {
        var updateLevel = await _levelRepo.FoundOrThrowAsync(id, Constants.ENTITY.LEVEL + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(level, updateLevel);
        await _levelServices.Update(updateLevel);
        return Ok(updateLevel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevel(Guid id)
    {
        await _levelRepo.FoundOrThrowAsync(id, Constants.ENTITY.LEVEL + Constants.ERROR.NOT_EXIST_ERROR);
        await _levelServices.Delete(id);
        return NoContent();
    }
}
