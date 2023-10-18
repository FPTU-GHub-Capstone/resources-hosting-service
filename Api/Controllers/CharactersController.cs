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
    public CharactersController(ICharacterServices characterServices, IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterServices = characterServices;
        _characterRepo = characterRepo;
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
        var newC = new CharacterEntity();
        Mapper.Map(character, newC);
        await _characterServices.Create(newC);
        return CreatedAtAction("GetCharacter", new { id = newC.Id }, newC);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(Guid id, [FromBody] UpdateCharacterRequest character)
    {
        var newC = await _characterRepo.FoundOrThrowAsync(id, "Character not exist.");
        Mapper.Map(character, newC);
        await _characterServices.Update(id, newC);
        return Ok(newC);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(Guid id)
    {
        await _characterRepo.FoundOrThrowAsync(id, "Character not exist.");
        await _characterServices.Delete(id);
        return NoContent();
    }
}
