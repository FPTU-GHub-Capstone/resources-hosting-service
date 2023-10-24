using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class WalletServices : IWalletServices
{
    public readonly IGenericRepository<WalletEntity> _walletRepo;
    public WalletServices(IGenericRepository<WalletEntity> walletRepo)
    {
        _walletRepo = walletRepo;
    }
    public async Task<ICollection<WalletEntity>> List()
    {
        return await _walletRepo.ListAsync();
    }
    public async Task<WalletEntity> GetById(Guid walletId)
    {
        return await _walletRepo.FindByIdAsync(walletId);
    }
    public async Task<ICollection<WalletEntity>> GetByWalletCategoryId(Guid id)
    {
        return await _walletRepo.WhereAsync(w => w.WalletCategoryId.Equals(id));
    }
    public async Task<ICollection<WalletEntity>> GetByCharacterId(Guid id)
    {
        return await _walletRepo.WhereAsync(w => w.CharacterId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _walletRepo.CountAsync();
    }
    public async Task Create(WalletEntity wallet) {
        await CheckDuplicateWallet(wallet);
        await _walletRepo.CreateAsync(wallet);
    }
    public async Task Update(WalletEntity wallet) {
        await CheckDuplicateWallet(wallet);
        await _walletRepo.UpdateAsync(wallet);
    }
    public async Task Delete(Guid walletId) {
        await _walletRepo.DeleteSoftAsync(walletId);
    }
    public async Task CheckDuplicateWallet(WalletEntity wallet)
    {
        var checkWallet = await _walletRepo.FirstOrDefaultAsync(
            w => w.CharacterId.Equals(wallet.CharacterId) && w.WalletCategoryId.Equals(wallet.WalletCategoryId));
        if (checkWallet is not null && (checkWallet.Id == Guid.Empty || checkWallet.Id != wallet.Id))
        {
            throw new NotFoundException(Constants.ENTITY.WALLET + Constants.ERROR.ALREADY_EXIST_ERROR);
        }
    }
}
