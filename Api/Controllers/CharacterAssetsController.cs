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

[Route(Constants.Http.API_VERSION + "/gms/character-assets")]
public class CharacterAssetsController : BaseController
{
    private readonly ICharacterAssetServices _characterAssetServices;
    public CharacterAssetsController(ICharacterAssetServices characterAssetServices)
    {
        _characterAssetServices = characterAssetServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterAssets([FromQuery] Guid? characterId)
    {
        RequiredScope("characterassets:*:get");
        return Ok(await _characterAssetServices.List(characterId));
    }
}