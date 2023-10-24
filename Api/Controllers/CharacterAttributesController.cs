using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Runtime.InteropServices;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/character-attributes")]
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
        var charAttList = await _charAttServices.List();
        return Ok(charAttList);
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
        await _charRepo.FoundOrThrowAsync(charAtt.CharacterId, Constants.ENTITY.CHARACTER + Constants.ERROR.NOT_EXIST_ERROR);
        await _attGrpRepo.FoundOrThrowAsync(charAtt.AttributeGroupId, Constants.ENTITY.ATTRIBUTE_GROUP + Constants.ERROR.NOT_EXIST_ERROR);
        var newCharAtt = new CharacterAttributeEntity();
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Create(newCharAtt);
        return CreatedAtAction(nameof(GetCharacterAttribute), new { id = newCharAtt.Id }, newCharAtt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAttribute(Guid id, [FromBody] UpdateCharacterAttributeRequest charAtt)
    {
        var newCharAtt = await _charAttRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_ATTRIBUTE + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Update(newCharAtt);
        return Ok(newCharAtt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAttribute(Guid id)
    {
        await _charAttRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_ATTRIBUTE + Constants.ERROR.NOT_EXIST_ERROR);
        await _charAttServices.Delete(id);
        return NoContent();
    }
}
