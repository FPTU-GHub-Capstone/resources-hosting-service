using DomainLayer.Constants;
using DomainLayer.Entities;
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
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    private readonly IGenericRepository<GameServerEntity> _gameServerRepo;
    public CharactersController(ICharacterServices characterServices, ICharacterAssetServices characterAssetServices
        , ICharacterAttributeServices characterAttributeServices, IWalletServices walletServices
        , ILevelProgressServices levelProgressServices
        , IGenericRepository<CharacterEntity> characterRepo, IGenericRepository<UserEntity> userRepo
        , IGenericRepository<CharacterTypeEntity> characterTypeRepo, IGenericRepository<GameServerEntity> gameServerRepo)
    {
        _characterServices = characterServices;
        _characterAssetServices = characterAssetServices;
        _characterAttributeServices = characterAttributeServices;
        _walletServices = walletServices;
        _levelProgressServices = levelProgressServices;
        _characterRepo = characterRepo;
        _userRepo = userRepo;
        _characterTypeRepo = characterTypeRepo;
        _gameServerRepo = gameServerRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacters()
    {
        var cList = await _characterServices.List();
        return Ok(cList);
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
        return Ok(await _walletServices.ListWalletByCharacterId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest character)
    {
        await _userRepo.FoundOrThrowAsync(character.UserId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        await _characterTypeRepo.FoundOrThrowAsync(character.CharacterTypeId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServerRepo.FoundOrThrowAsync(character.GameServerId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        var newC = new CharacterEntity();
        Mapper.Map(character, newC);
        await _characterServices.Create(newC);
        return CreatedAtAction(nameof(GetCharacter), new { id = newC.Id }, newC);
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