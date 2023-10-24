using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/asset-types")]
public class AssetTypesController : BaseController
{
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public AssetTypesController(IAssetTypeServices assetTypeServices, IGenericRepository<AssetTypeEntity> assetTypeRepo, IGenericRepository<GameEntity> gameRepo)
    {
        _assetTypeServices = assetTypeServices;
        _assetTypeRepo = assetTypeRepo;
        _gameRepo = gameRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetTypes()
    {
        var assetTypeList = await _assetTypeServices.List();
        return Ok(assetTypeList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssetType(Guid id)
    {
        var assetType = await _assetTypeServices.GetById(id);
        return Ok(assetType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssetType([FromBody] CreateAssetTypeRequest assetType)
    {
        await _gameRepo.FoundOrThrowAsync(assetType.GameId, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        var cAssetType = new AssetTypeEntity();
        Mapper.Map(assetType, cAssetType);
        await _assetTypeServices.Create(cAssetType);
        return CreatedAtAction(nameof(GetAssetType), new {id = cAssetType.Id}, cAssetType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssetType(Guid id, [FromBody] UpdateAssetTypeRequest assetType)
    {
        var uAssetType = await _assetTypeRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(assetType, uAssetType);
        await _assetTypeServices.Update(uAssetType);
        return Ok(uAssetType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetType(Guid id)
    {
        await _assetTypeRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        await _assetTypeServices.Delete(id);
        return NoContent();
    }
}
