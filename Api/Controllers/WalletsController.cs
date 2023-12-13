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
        if (!CurrentScp.Contains("wallets:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _walletServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWallet(Guid id)
    {
        return Ok(await _walletServices.GetById(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateWalletRequest wallet)
    {
        var updateWallet = await _walletRepo.FoundOrThrowAsync(id, Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(wallet, updateWallet);
        await _walletServices.Update(updateWallet);
        return Ok(updateWallet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _walletRepo.FoundOrThrowAsync(id, Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
        await _walletServices.Delete(id);
        return NoContent();
    }
}