﻿using DomainLayer.Constants;
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
    public AssetTypesController(IAssetTypeServices assetTypeServices)
    {
        _assetTypeServices = assetTypeServices;
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
        var cAssetType = new AssetTypeEntity();
        Mapper.Map(assetType, cAssetType);
        await _assetTypeServices.Create(cAssetType);
        return CreatedAtAction("GetAssetType", new {id = cAssetType.Id}, cAssetType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssetType(Guid id, [FromBody] UpdateAssetTypeRequest assetType)
    {
        var uAssetType = await _assetTypeServices.GetById(id);
        Mapper.Map(assetType, uAssetType);
        await _assetTypeServices.Update(id, uAssetType);
        return Ok(uAssetType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetType(Guid id)
    {
        await _assetTypeServices.Delete(id);
        return NoContent();
    }
}
