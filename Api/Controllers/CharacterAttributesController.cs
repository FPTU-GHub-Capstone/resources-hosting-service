using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Runtime.InteropServices;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/character-attributes")]
public class CharacterAtributesController : BaseController
{
    private readonly ICharacterAttributeServices _charAttServices;
    private readonly IGenericRepository<CharacterAttributeEntity> _charAttRepo;
    private readonly IGenericRepository<CharacterEntity> _charRepo;
    private readonly IGenericRepository<AttributeGroupEntity> _attGrpRepo;
    public CharacterAtributesController(ICharacterAttributeServices charAttServices, IGenericRepository<CharacterAttributeEntity> charAttRepo
        , IGenericRepository<CharacterEntity> charRepo, IGenericRepository<AttributeGroupEntity> attGrpRepo)
    {
        _charAttServices = charAttServices;
        _charAttRepo = charAttRepo;
        _charRepo = charRepo;
        _attGrpRepo = attGrpRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetCharacterAttributes()
    {
        if (CurrentScp.Contains("characterattributes:*:get"))
        {
            return Ok(await _charAttServices.List());
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterAttribute(Guid id)
    {
        var charAtt = await _charAttServices.GetById(id);
        return Ok(charAtt);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacterAttribute([FromBody] CreateCharacterAttributeRequest charAtt)
    {
        await _charRepo.FoundOrThrowAsync(charAtt.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _attGrpRepo.FoundOrThrowAsync(charAtt.AttributeGroupId, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        var newCharAtt = new CharacterAttributeEntity();
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Create(newCharAtt);
        return CreatedAtAction(nameof(GetCharacterAttribute), new { id = newCharAtt.Id }, newCharAtt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAttribute(Guid id, [FromBody] UpdateCharacterAttributeRequest charAtt)
    {
        var newCharAtt = await _charAttRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Update(newCharAtt);
        return Ok(newCharAtt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAttribute(Guid id)
    {
        await _charAttRepo.FoundOrThrowAsync(id, Constants.Entities.CHARACTER_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        await _charAttServices.Delete(id);
        return NoContent();
    }
}