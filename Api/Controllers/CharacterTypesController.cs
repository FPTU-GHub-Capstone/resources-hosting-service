using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Text.Json.Nodes;
using WebApiLayer.UserFeatures.Requests;
using WebApiLayer.UserFeatures.Response;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/character-types")]
public class CharacterTypesController : BaseController
{
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public CharacterTypesController(ICharacterTypeServices characterTypeServices, IGenericRepository<CharacterTypeEntity> characterTypeRepo
, IGenericRepository<GameEntity> gameRepo)
    {
        _characterTypeServices = characterTypeServices;
        _characterTypeRepo = characterTypeRepo;
        _gameRepo = gameRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterTypes()
    {
        var ctList = await _characterTypeServices.List();
        List<CharacterTypeResponse> ctListResponse = new();
        foreach (var ct in ctList)
        {
            var ctResponse = new CharacterTypeResponse();
            Mapper.Map(ct, ctResponse);
            ctResponse.BaseProperties = JsonObject.Parse(ct.BaseProperties);
            ctListResponse.Add(ctResponse);
        }
        return Ok(ctListResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterType(Guid id)
    {
        var ct = await _characterTypeServices.GetById(id);
        return Ok(ct);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacterType([FromBody] CreateCharacterTypeRequest charType)
    {
        await _gameRepo.FoundOrThrowAsync(charType.GameId, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var newCT = new CharacterTypeEntity();
        Mapper.Map(charType, newCT);
        newCT.BaseProperties = charType.BaseProperties.ToString();
        await _characterTypeServices.Create(newCT);
        return CreatedAtAction(nameof(GetCharacterType), new { id = newCT.Id }, newCT);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterType(Guid id, [FromBody] UpdateCharacterTypeRequest charType)
    {
        var ct = await _characterTypeRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charType, ct);
        await _characterTypeServices.Update(ct);
        return Ok(ct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterType(Guid id)
    {
        await _characterTypeRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _characterTypeServices.Delete(id);
        return NoContent();
    }
}