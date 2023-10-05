using DomainLayer.Entities;
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
    public async Task<ICollection<WalletEntity>> GetByPaymentId(Guid id)
    {
        return await _walletRepo.WhereAsync(w => w.PaymentId.Equals(id));
    }
    public async Task<int> Count()
    {
        return await _walletRepo.CountAsync();
    }
    public async Task Create(WalletEntity wallet) { }
    public async Task Update(Guid walletId, WalletEntity wallet) { }
    public async Task Delete(Guid walletId) { }
}
