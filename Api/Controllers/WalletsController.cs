using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/wallets")]
public class WalletsController : BaseController
{
    private readonly IWalletServices _walletServices;
    private readonly IGenericRepository<WalletEntity> _walletRepo;
    public WalletsController(IWalletServices walletServices, IGenericRepository<WalletEntity> walletRepo)
    {
        _walletServices = walletServices;
        _walletRepo = walletRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        return Ok(await _walletServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWallet(Guid id)
    {
        return Ok(await _walletServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest wallet)
    {
        var newWallet = new WalletEntity();
        Mapper.Map(wallet, newWallet);
        await _walletServices.Create(newWallet);
        return CreatedAtAction("GetWallet", new { id = newWallet.Id }, newWallet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateWalletRequest wallet)
    {
        var updateWallet = await _walletRepo.FoundOrThrowAsync(id, "Wallet not exist");
        Mapper.Map(wallet, updateWallet);
        await _walletServices.Update(updateWallet);
        return Ok(updateWallet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _walletRepo.FoundOrThrowAsync(id, "Wallet not exist");
        await _walletServices.Delete(id);
        return NoContent();
    }
}
