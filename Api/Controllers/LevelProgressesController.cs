using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/level-progresses")]
public class LevelProgressesController : BaseController
{
    private readonly ILevelProgressServices _levelProgressServices;
    private readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    private readonly IGenericRepository<CharacterEntity> _levelRepo;
    public LevelProgressesController(ILevelProgressServices levelProgressServices, IGenericRepository<LevelProgressEntity> levelProgressRepo
        , IGenericRepository<CharacterEntity> characterRepo, IGenericRepository<CharacterEntity> levelRepo)
    {
        _levelProgressServices = levelProgressServices;
        _levelProgressRepo = levelProgressRepo;
        _characterRepo = characterRepo;
        _levelRepo = levelRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetLevelProgresses()
    {
        return Ok(await _levelProgressServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLevelProgress(Guid id)
    {
        return Ok(await _levelProgressServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLevelProgress([FromBody] CreateLevelProgressRequest levelProg)
    {
        await _characterRepo.FoundOrThrowAsync(levelProg.CharacterId, Constants.ENTITY.CHARACTER + Constants.ERROR.NOT_EXIST_ERROR);
        await _levelRepo.FoundOrThrowAsync(levelProg.LevelId, Constants.ENTITY.LEVEL + Constants.ERROR.NOT_EXIST_ERROR);
        var newLevelProg = new LevelProgressEntity();
        Mapper.Map(levelProg, newLevelProg);
        await _levelProgressServices.Create(newLevelProg);
        return CreatedAtAction(nameof(GetLevelProgress),new {id = newLevelProg.Id}, newLevelProg);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLevelProgress(Guid id, [FromBody] UpdateLevelProgressRequest levelProg)
    {
        var updateLevelProg = await _levelProgressRepo.FoundOrThrowAsync(id, Constants.ENTITY.LEVEL_PROGRESS + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(levelProg, updateLevelProg);
        await _levelProgressServices.Update(updateLevelProg);
        return Ok(updateLevelProg);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLevelProgress(Guid id)
    {
        await _levelProgressRepo.FoundOrThrowAsync(id, Constants.ENTITY.LEVEL_PROGRESS + Constants.ERROR.NOT_EXIST_ERROR);
        await _levelProgressServices.Delete(id);
        return NoContent();
    }
}
