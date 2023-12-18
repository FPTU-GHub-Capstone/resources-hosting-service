using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/wallets")]
public class WalletsController : BaseController
{
    private readonly IWalletServices _walletServices;
    private readonly IGenericRepository<WalletEntity> _walletRepo;
    private readonly IGenericRepository<WalletCategoryEntity> _walletCatRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    public WalletsController(IWalletServices walletServices, IGenericRepository<WalletEntity> walletRepo
        , IGenericRepository<WalletCategoryEntity> walletCatRepo, IGenericRepository<CharacterEntity> characterRepo)
    {
        _walletServices = walletServices;
        _walletRepo = walletRepo;
        _walletCatRepo = walletCatRepo;
        _characterRepo = characterRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        RequiredScope("games:*:get", "wallets:*:get");
        return Ok(await _walletServices.List());
    }
}