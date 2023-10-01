using Application.Interfaces;
using Domain.Entities.Character;
using Domain.Entities.Wallet;
using System.Collections.ObjectModel;
using System;

namespace Application.Services.WalletServices;

public class WalletServices : IWalletServices
{
    public readonly IGenericRepository<WalletCategory> _walletCategoryRepo;
    public readonly IGenericRepository<WalletEntity> _walletRepo;
    public WalletServices(IGenericRepository<WalletCategory> walletCategoryRepo, IGenericRepository<WalletEntity> walletRepo)
    {
        _walletCategoryRepo = walletCategoryRepo;
        _walletRepo = walletRepo;
    }
    //Wallet Category
    public async Task<ICollection<WalletCategory>> GetWalletCategories()
    {
        var walCat = await _walletCategoryRepo.ListAsync();
        return walCat;
    }
    public async Task<WalletCategory> GetWalletCategory(Guid categoryId)
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
    public async Task<ICollection<WalletCategory>> GetWalletCategories(Guid gameId)
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
    public async Task<int> CountWalletCategories() {
        var walCat = await _walletCategoryRepo.ListAsync();
        return walCat.Count;
    }
    public async Task CreateWalletCategory(WalletCategory walletCategory) { }
    public async Task UpdateWalletCategory(Guid categoryId, WalletCategory walletCategory) { }
    public async Task DeleteWalletCategory(Guid categoryId) { }
    //Wallet Entity
    public async Task<ICollection<WalletEntity>> GetWallets()
    {
        var wal = await _walletRepo.ListAsync();
        return wal;
    }
    public async Task<WalletEntity> GetWallet(Guid walletId)
    {
        var wal = await _walletRepo.FindByIdAsync(walletId);
        if (wal == null)
        {
            throw new Exception($"Wallet not exist");
        }
        else
        {
            return wal;
        }
    }
    public async Task<ICollection<WalletEntity>> GetWallets(Guid id, int typeId)
    { //typeId: 1: WalletCategoryId, 2: CharacterId, 3: PaymentId
        ICollection<WalletEntity> wallets = new Collection<WalletEntity>();
        if (typeId == 1)
        {
            wallets = await _walletRepo.WhereAsync(
                w => w.WalletCategoryId.Equals(id));
        }
        else if (typeId == 2)
        {
            wallets = await _walletRepo.WhereAsync(
                w => w.CharacterId.Equals(id));
        }
        else if (typeId == 3)
        {
            wallets = await _walletRepo.WhereAsync(
                w => w.PaymentId.Equals(id));
        }
        if (wallets.Count == 0 || wallets == null)
        {
            throw new Exception($"Wallets or wallet category/character/payment not found");
        }
        else
        {
            return wallets;
        }
    }
    public async Task<int> CountWallets()
    {
        var wal = await _walletRepo.ListAsync();
        return wal.Count;
    }
    public async Task CreateWallet(WalletEntity wallet) { }
    public async Task UpdateWallet(Guid walletId, WalletEntity wallet) { }
    public async Task DeleteWallet(Guid walletId) { }
}
