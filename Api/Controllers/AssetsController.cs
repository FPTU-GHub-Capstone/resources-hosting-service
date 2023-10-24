﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/assets")]
public class AssetsController : BaseController
{
    private readonly IAssetServices _assetServices;
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    private readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;
    public AssetsController(IAssetServices assetServices, IGenericRepository<AssetEntity> assetRepo, IGenericRepository<AssetTypeEntity> assetTypeRepo)
    {
        _assetServices = assetServices;
        _assetRepo = assetRepo;
        _assetTypeRepo = assetTypeRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssets()
    {
        return Ok(await _assetServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsset(Guid id)
    {
        return Ok(await _assetServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest asset)
    {
        await _assetTypeRepo.FoundOrThrowAsync(asset.AssetTypeId, Constants.ENTITY.ASSET_TYPE + Constants.ERROR.NOT_EXIST_ERROR);
        var newAsset = new AssetEntity();
        Mapper.Map(asset, newAsset);
        await _assetServices.Create(newAsset);
        return CreatedAtAction(nameof(GetAsset), new { id = newAsset.Id }, newAsset);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset(Guid id, [FromBody] UpdateAssetRequest asset)
    {
        var updateAsset = await _assetRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(asset, updateAsset);
        await _assetServices.Update(updateAsset);
        return Ok(updateAsset);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(Guid id)
    {
        await _assetRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        await _assetServices.Delete(id);
        return NoContent();
    }
}