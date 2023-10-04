using Application.Interfaces;
using Domain.Entities.Wallet;
using System.Collections.ObjectModel;
using System;

namespace Application.Services.WalletServices;

public class WalletCategoryServices : IWalletCategoryServices
{
    public readonly IGenericRepository<WalletCategory> _walletCategoryRepo;
    public WalletCategoryServices(IGenericRepository<WalletCategory> walletCategoryRepo)
    {
        _walletCategoryRepo = walletCategoryRepo;
    }
    public async Task<ICollection<WalletCategory>> List()
    {
        return await _walletCategoryRepo.ListAsync();
    }
    public async Task<WalletCategory> GetById(Guid categoryId)
    {
        return await _walletCategoryRepo.FindByIdAsync(categoryId);
    }
    public async Task<ICollection<WalletCategory>> GetByGameId(Guid gameId)
    {
        return await _walletCategoryRepo.WhereAsync(wc => wc.GameId.Equals(gameId));
    }
    public async Task<int> Count() {
        return await _walletCategoryRepo.CountAsync();
    }
    public async Task Create(WalletCategory walletCategory) { }
    public async Task Update(Guid categoryId, WalletCategory walletCategory) { }
    public async Task Delete(Guid categoryId) { }
}
