using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/assets")]
public class AssetsController : BaseController
{
    private readonly IAssetServices _assetServices;
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    public AssetsController(IAssetServices assetServices, IGenericRepository<AssetEntity> assetRepo)
    {
        _assetServices = assetServices;
        _assetRepo = assetRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssets()
    {
        RequiredScope("assets:*:get");
        return Ok(await _assetServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsset(Guid id)
    {
        return Ok(await _assetServices.GetById(id));
    }

    [HttpGet("{id}/games")]
    public async Task<IActionResult> GetAssetByGameID(Guid id)
    {
        return Ok(await _assetServices.ListAssetsByGameId(id));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] UpdateAssetRequest asset)
    {
        var updateAsset = await _assetRepo.FoundOrThrowAsync(id, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(asset, updateAsset);
        await _assetServices.Update(updateAsset);
        return Ok(updateAsset);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(Guid id)
    {
        await _assetRepo.FoundOrThrowAsync(id, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _assetServices.Delete(id);
        return NoContent();
    }
}