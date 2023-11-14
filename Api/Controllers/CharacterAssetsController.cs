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
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    public CharacterAssetsController(ICharacterAssetServices characterAssetServices, IGenericRepository<CharacterAssetEntity> characterAssetRepo
        , IGenericRepository<AssetEntity> assetRepo, IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterAssetServices = characterAssetServices;
        _characterAssetRepo = characterAssetRepo;
        _assetRepo = assetRepo;
        _characterRepo = characterRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetCharacterAssets([FromQuery] Guid? characterId)
    {
        var cList = await _characterAssetServices.List(characterId);
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
        await _assetRepo.FoundOrThrowAsync(charAss.AssetsId, Constants.ENTITY.ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(charAss.CharacterId, Constants.ENTITY.CHARACTER + Constants.ERROR.NOT_EXIST_ERROR);
        var newCharAss = new CharacterAssetEntity();
        Mapper.Map(charAss, newCharAss);
        await _characterAssetServices.Create(newCharAss);
        return CreatedAtAction(nameof(GetCharacterAsset), new { id = newCharAss.Id }, newCharAss);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacterAsset(Guid id, [FromBody] UpdateCharacterAssetRequest charAss)
    {
        var updateCharAss = await _characterAssetRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(charAss, updateCharAss);
        await _characterAssetServices.Update(updateCharAss);
        return Ok(updateCharAss);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacterAsset(Guid id)
    {
        await _characterAssetRepo.FoundOrThrowAsync(id, Constants.ENTITY.CHARACTER_ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        await _characterAssetServices.Delete(id);
        return NoContent();
    }
}