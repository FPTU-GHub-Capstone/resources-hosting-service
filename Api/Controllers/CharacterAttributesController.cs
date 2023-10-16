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
    public CharacterAtributesController(ICharacterAttributeServices charAttServices, IGenericRepository<CharacterAttributeEntity> charAttRepo)
    {
        _charAttServices = charAttServices;
        _charAttRepo = charAttRepo;
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
        var newCharAtt = new CharacterAttributeEntity();
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Create(newCharAtt);
        return CreatedAtAction("GetCharacterAttribute", new { id = newCharAtt.Id }, newCharAtt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAttribute(Guid id, [FromBody] UpdateCharacterAttributeRequest charAtt)
    {
        var newCharAtt = await _charAttServices.GetById(id);
        Mapper.Map(charAtt, newCharAtt);
        await _charAttServices.Update(id, newCharAtt);
        return Ok(newCharAtt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAttribute(Guid id)
    {
        await _charAttServices.Delete(id);
        return NoContent();
    }
}
