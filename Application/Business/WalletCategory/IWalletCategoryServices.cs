using DomainLayer.Entities;

namespace ServiceLayer.Business.WalletCategory;
public interface IWalletCategoryServices
{
    Task<ICollection<WalletCategoryEntity>> List();
    Task<WalletCategoryEntity> GetById(Guid categoryId);
    Task<ICollection<WalletCategoryEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(WalletCategoryEntity walletCategory);
    Task Update(Guid categoryId, WalletCategoryEntity walletCategory);
    Task Delete(Guid categoryId);
}
