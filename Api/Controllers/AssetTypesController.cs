using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/asset-types")]
public class AssetTypesController : BaseController
{
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;
    public AssetTypesController(IAssetTypeServices assetTypeServices, IGenericRepository<AssetTypeEntity> assetTypeRepo)
    {
        _assetTypeServices = assetTypeServices;
        _assetTypeRepo = assetTypeRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetTypes()
    {
        if (!CurrentScp.Contains("assettypes:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _assetTypeServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssetType(Guid id)
    {
        var assetType = await _assetTypeServices.GetById(id);
        return Ok(assetType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssetType(Guid id, [FromBody] UpdateAssetTypeRequest assetType)
    {
        var uAssetType = await _assetTypeRepo.FoundOrThrowAsync(id, Constants.Entities.ASSET_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(assetType, uAssetType);
        await _assetTypeServices.Update(uAssetType);
        return Ok(uAssetType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetType(Guid id)
    {
        await _assetTypeRepo.FoundOrThrowAsync(id, Constants.Entities.ASSET_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _assetTypeServices.Delete(id);
        return NoContent();
    }
}