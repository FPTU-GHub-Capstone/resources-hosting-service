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
    public AssetsController(IAssetServices assetServices)
    {
        _assetServices = assetServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssets()
    {
        RequiredScope("assets:*:get");
        return Ok(await _assetServices.List());
    }
}