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
        RequiredScope("games:*:get", "charactertypes:*:get");
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
}