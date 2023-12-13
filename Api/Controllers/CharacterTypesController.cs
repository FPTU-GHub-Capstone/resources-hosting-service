using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
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
    public CharacterTypesController(ICharacterTypeServices characterTypeServices, IGenericRepository<CharacterTypeEntity> characterTypeRepo)
    {
        _characterTypeServices = characterTypeServices;
        _characterTypeRepo = characterTypeRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterTypes()
    {
        RequiredScope("charactertypes:*:get");
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