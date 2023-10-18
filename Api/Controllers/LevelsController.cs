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
    public LevelsController(ILevelServices levelServices, IGenericRepository<LevelEntity> levelRepo)
    {
        _levelServices = levelServices;
        _levelRepo = levelRepo;
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
        var newLevel = new LevelEntity();
        Mapper.Map(level, newLevel);
        await _levelServices.Create(newLevel);
        return CreatedAtAction("GetLevel", new { id = newLevel.Id }, newLevel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLevel(Guid id, [FromBody] UpdateLevelsController level)
    {
        var updateLevel = await _levelRepo.FoundOrThrowAsync(id, "Level not exist.");
        Mapper.Map(level, updateLevel);
        await _levelServices.Update(id, updateLevel);
        return Ok(updateLevel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevel(Guid id)
    {
        await _levelRepo.FoundOrThrowAsync(id, "Level not exist.");
        await _levelServices.Delete(id);
        return NoContent();
    }
}
