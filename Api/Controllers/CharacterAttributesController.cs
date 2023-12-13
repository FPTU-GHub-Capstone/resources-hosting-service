using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
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
    public CharacterAtributesController(ICharacterAttributeServices charAttServices, IGenericRepository<CharacterAttributeEntity> charAttRepo)
    {
        _charAttServices = charAttServices;
        _charAttRepo = charAttRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetCharacterAttributes()
    {
        if (!CurrentScp.Contains("characterattributes:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _charAttServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterAttribute(Guid id)
    {
        var charAtt = await _charAttServices.GetById(id);
        return Ok(charAtt);
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