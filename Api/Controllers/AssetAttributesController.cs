using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/asset-attributes")]
public class AssetAttributesController : BaseController
{
    private readonly IAssetAttributeServices _assetAttServices;
    public AssetAttributesController(IAssetAttributeServices assetAttServices)
    {
        _assetAttServices = assetAttServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetAttributes()
    {
        RequiredScope("assetattributes:*:get");
        return Ok(await _assetAttServices.List());
    }
}