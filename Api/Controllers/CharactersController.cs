using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/characters")]
public class CharactersController : BaseController
{
    private readonly ICharacterServices _characterServices;
    private readonly ICharacterAssetServices _characterAssetServices;
    private readonly ICharacterAttributeServices _characterAttributeServices;
    private readonly IWalletServices _walletServices;
    private readonly ILevelProgressServices _levelProgressServices;
    public CharactersController(ICharacterServices characterServices, ICharacterAssetServices characterAssetServices
        , ICharacterAttributeServices characterAttributeServices, IWalletServices walletServices
        , ILevelProgressServices levelProgressServices)
    {
        _characterServices = characterServices;
        _characterAssetServices = characterAssetServices;
        _characterAttributeServices = characterAttributeServices;
        _walletServices = walletServices;
        _levelProgressServices = levelProgressServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacters()
    {
        RequiredScope("characters:*:get");
        return Ok(await _characterServices.List());
    }

    [HttpGet("{id}/character-assets")]
    public async Task<IActionResult> GetCharAssetByCharID(Guid id)
    {
        var charAssList = await _characterAssetServices.ListCharAssetsByCharId(id);
        return Ok(charAssList);
    }

    [HttpGet("{id}/character-attributes")]
    public async Task<IActionResult> GetCharAttByCharID(Guid id)
    {
        return Ok(await _characterAttributeServices.ListCharAttByCharId(id));
    }

    [HttpGet("{id}/level-progress")]
    public async Task<IActionResult> GetLevelProgressByCharID(Guid id)
    {
        return Ok(await _levelProgressServices.ListLevelProgByCharacterId(id));
    }

    [HttpGet("{id}/wallet")]
    public async Task<IActionResult> GetWalletByCharID(Guid id)
    {
        return Ok(await _walletServices.ListWalletsByCharacterId(id));
    }
}