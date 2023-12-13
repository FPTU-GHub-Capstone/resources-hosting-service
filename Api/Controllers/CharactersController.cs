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
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    public CharactersController(ICharacterServices characterServices, ICharacterAssetServices characterAssetServices
        , ICharacterAttributeServices characterAttributeServices, IWalletServices walletServices
        , ILevelProgressServices levelProgressServices
        , IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterServices = characterServices;
        _characterAssetServices = characterAssetServices;
        _characterAttributeServices = characterAttributeServices;
        _walletServices = walletServices;
        _levelProgressServices = levelProgressServices;
        _characterRepo = characterRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacters()
    {
        if (!CurrentScp.Contains("characters:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _characterServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacter(Guid id)
    {
        var character = await _characterServices.GetById(id);
        return Ok(character);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(Guid id, [FromBody] UpdateCharacterRequest character)
    {
        var updateC = await _characterRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(character, updateC);
        await _characterServices.Update(updateC);
        return Ok(updateC);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(Guid id)
    {
        await _characterRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _characterServices.Delete(id);
        return NoContent();
    }
}