using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class WalletCategoryServices : IWalletCategoryServices
{
    public readonly IGenericRepository<WalletCategoryEntity> _walletCategoryRepo;
    public WalletCategoryServices(IGenericRepository<WalletCategoryEntity> walletCategoryRepo)
    {
        _walletCategoryRepo = walletCategoryRepo;
    }
    public async Task<ICollection<WalletCategoryEntity>> List()
    {
        return await _walletCategoryRepo.ListAsync();
    }
    public async Task<WalletCategoryEntity> GetById(Guid categoryId)
    {
        return await _walletCategoryRepo.FindByIdAsync(categoryId);
    }
    public async Task<ICollection<WalletCategoryEntity>> GetByGameId(Guid gameId)
    {
        return await _walletCategoryRepo.WhereAsync(wc => wc.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _walletCategoryRepo.CountAsync();
    }
    public async Task Create(WalletCategoryEntity walletCategory) { }
    public async Task Update(Guid categoryId, WalletCategoryEntity walletCategory) { }
    public async Task Delete(Guid categoryId) { }
}
