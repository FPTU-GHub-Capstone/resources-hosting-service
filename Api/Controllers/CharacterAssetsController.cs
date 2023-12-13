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
    private readonly IGenericRepository<CharacterAssetEntity> _characterAssetRepo;
    public CharacterAssetsController(ICharacterAssetServices characterAssetServices, IGenericRepository<CharacterAssetEntity> characterAssetRepo)
    {
        _characterAssetServices = characterAssetServices;
        _characterAssetRepo = characterAssetRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterAssets([FromQuery] Guid? characterId)
    {
        RequiredScope("characterassets:*:get");
        return Ok(await _characterAssetServices.List(characterId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterAsset(Guid id)
    {
        var character = await _characterAssetServices.GetById(id);
        return Ok(character);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAsset(Guid id, [FromBody] UpdateCharacterAssetRequest charAss)
    {
        var updateCharAss = await _characterAssetRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_ASSET + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charAss, updateCharAss);
        await _characterAssetServices.Update(updateCharAss);
        return Ok(updateCharAss);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAsset(Guid id)
    {
        await _characterAssetRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _characterAssetServices.Delete(id);
        return NoContent();
    }
}