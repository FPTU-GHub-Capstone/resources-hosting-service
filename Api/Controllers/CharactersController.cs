using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/characters")]
public class CharactersController : BaseController
{
    private readonly ICharacterServices _characterServices;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    private readonly IGenericRepository<GameServerEntity> _gameServerRepo;
    public CharactersController(ICharacterServices characterServices, IGenericRepository<CharacterEntity> characterRepo, IGenericRepository<UserEntity> userRepo
        , IGenericRepository<CharacterTypeEntity> characterTypeRepo, IGenericRepository<GameServerEntity> gameServerRepo)
    {
        _characterServices = characterServices;
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

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest character)
    {
        await _userRepo.FoundOrThrowAsync(character.UserId, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        await _characterTypeRepo.FoundOrThrowAsync(character.CharacterTypeId, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        await _gameServerRepo.FoundOrThrowAsync(character.GameServerId, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        var newC = new CharacterEntity();
        Mapper.Map(character, newC);
        await _characterServices.Create(newC);
        return CreatedAtAction(nameof(GetCharacter), new { id = newC.Id }, newC);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(Guid id, [FromBody] UpdateCharacterRequest character)
    {
        var updateC = await _characterRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(character, updateC);
        await _characterServices.Update(updateC);
        return Ok(updateC);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(Guid id)
    {
        await _characterRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER + Constants.ERROR.NOT_EXIST_ERROR);
        await _characterServices.Delete(id);
        return NoContent();
    }
}