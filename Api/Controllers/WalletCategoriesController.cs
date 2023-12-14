using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/wallet-category")]
public class WalletCategoriesController : BaseController
{
    private readonly IWalletCategoryServices _walletCategoryServices;

    public WalletCategoriesController(IWalletCategoryServices walletCategoryServices)
    {
        _walletCategoryServices = walletCategoryServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetWalletCategories()
    {
        RequiredScope("walletcategories:*:get");
        return Ok(await _walletCategoryServices.List());
    }
}