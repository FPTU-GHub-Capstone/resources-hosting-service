﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/wallet-category")]
public class WalletCategoriesController : BaseController
{
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IGenericRepository<WalletCategoryEntity> _walletCategoryRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;

    public WalletCategoriesController(IWalletCategoryServices walletCategoryServices, IGenericRepository<WalletCategoryEntity> walletCategoryRepo
, IGenericRepository<GameEntity> gameRepo)
    {
        _walletCategoryServices = walletCategoryServices;
        _walletCategoryRepo = walletCategoryRepo;
        _gameRepo = gameRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetWalletCategories()
    {
        return Ok(await _walletCategoryServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWalletCategory(Guid id)
    {
        return Ok(await _walletCategoryServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateWalletCategory([FromBody] CreateWalletCategoryRequest walCat)
    {
        await _gameRepo.FoundOrThrowAsync(walCat.GameId, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        var newWalCat = new WalletCategoryEntity();
        Mapper.Map(walCat, newWalCat);
        await _walletCategoryServices.Create(newWalCat);
        return CreatedAtAction(nameof(GetWalletCategory), new { id = newWalCat.Id }, newWalCat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWalletCategory(Guid id, [FromBody] UpdateWalletCategoryRequest walCat)
    {
        var updateWalCat = await _walletCategoryRepo.FoundOrThrowAsync(id, Constants.ENTITY.WALLET + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(walCat, updateWalCat);
        await _walletCategoryServices.Update(updateWalCat);
        return Ok(updateWalCat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWalletCategory(Guid id)
    {
        await _walletCategoryRepo.FoundOrThrowAsync(id, Constants.ENTITY.WALLET + Constants.ERROR.NOT_EXIST_ERROR);
        await _walletCategoryServices.Delete(id);
        return NoContent();
    }
}