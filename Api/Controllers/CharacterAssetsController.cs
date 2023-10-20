using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/character-assets")]
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
    public async Task<IActionResult> GetCharacterAssets()
    {
        var cList = await _characterAssetServices.List();
        return Ok(cList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterAsset(Guid id)
    {
        var character = await _characterAssetServices.GetById(id);
        return Ok(character);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacterAsset([FromBody] CreateCharacterAssetRequest charAss)
    {
        var newCharAss = new CharacterAssetEntity();
        Mapper.Map(charAss, newCharAss);
        await _characterAssetServices.Create(newCharAss);
        return CreatedAtAction("GetCharacterAsset", new {id = newCharAss.Id}, newCharAss);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAsset(Guid id, [FromBody] UpdateCharacterAssetRequest charAss)
    {
        var updateCharAss = await _characterAssetRepo.FoundOrThrowAsync(id,"Character Asset not exist");
        Mapper.Map(charAss, updateCharAss);
        await _characterAssetServices.Update(updateCharAss);
        return Ok(updateCharAss);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAsset(Guid id)
    {
        await _characterAssetRepo.FoundOrThrowAsync(id, "Character Asset not exist");
        await _characterAssetServices.Delete(id);
        return NoContent();
    }
}
