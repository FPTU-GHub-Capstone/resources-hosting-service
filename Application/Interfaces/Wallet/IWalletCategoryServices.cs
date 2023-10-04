using Domain.Entities;

namespace Application.Interfaces;
public interface IWalletCategoryServices 
{
    Task<ICollection<WalletCategory>> List();
    Task<WalletCategory> GetById(Guid categoryId);
    Task<ICollection<WalletCategory>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(WalletCategory walletCategory);
    Task Update(Guid categoryId, WalletCategory walletCategory);
    Task Delete(Guid categoryId);
}
