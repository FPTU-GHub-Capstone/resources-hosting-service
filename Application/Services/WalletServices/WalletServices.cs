using Application.Interfaces;
using Domain.Entities.Character;
using Domain.Entities.Wallet;
using System.Collections.ObjectModel;
using System;

namespace Application.Services.WalletServices;

public class WalletServices : IWalletServices
{
    public readonly IGenericRepository<WalletEntity> _walletRepo;
    public WalletServices(IGenericRepository<WalletEntity> walletRepo)
    {
        _walletRepo = walletRepo;
    }
    public async Task<ICollection<WalletEntity>> List()
    {
        var wal = await _walletRepo.ListAsync();
        return wal;
    }
    public async Task<WalletEntity> GetById(Guid walletId)
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
    public async Task<ICollection<WalletEntity>> GetById(Guid id, int typeId)
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
    public async Task<int> Count()
    {
        return await _walletRepo.CountAsync();
    }
    public async Task Create(WalletEntity wallet) { }
    public async Task Update(Guid walletId, WalletEntity wallet) { }
    public async Task Delete(Guid walletId) { }
}
