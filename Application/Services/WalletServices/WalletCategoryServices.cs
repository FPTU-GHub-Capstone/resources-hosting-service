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
        var walCat = await _walletCategoryRepo.ListAsync();
        return walCat;
    }
    public async Task<WalletCategory> GetById(Guid categoryId)
    {
        var walCat = await _walletCategoryRepo.FindByIdAsync(categoryId);
        if (walCat == null)
        {
            throw new Exception($"Wallet category not exist");
        }
        else
        {
            return walCat;
        }
    }
    public async Task<ICollection<WalletCategory>> GetByGameId(Guid gameId)
    {
        var walCat = await _walletCategoryRepo.WhereAsync(
            wc => wc.GameId.Equals(gameId));
        if (walCat.Count == 0)
        {
            throw new Exception($"Wallet category or game not found");
        }
        else
        {
            return walCat;
        }
    }
    public async Task<int> Count() {
        return await _walletCategoryRepo.CountAsync();
    }
    public async Task Create(WalletCategory walletCategory) { }
    public async Task Update(Guid categoryId, WalletCategory walletCategory) { }
    public async Task Delete(Guid categoryId) { }
}
