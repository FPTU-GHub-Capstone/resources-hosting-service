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
    public AssetTypesController(IAssetTypeServices assetTypeServices)
    {
        _assetTypeServices = assetTypeServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetTypes()
    {
        RequiredScope("assettypes:*:get");
        return Ok(await _assetTypeServices.List());
    }
}