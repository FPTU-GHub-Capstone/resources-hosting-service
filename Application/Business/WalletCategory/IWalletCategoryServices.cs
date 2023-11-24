using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IWalletCategoryServices
{
    Task<ICollection<WalletCategoryEntity>> List();
    Task<WalletCategoryEntity> GetById(Guid categoryId);
    Task<ICollection<WalletCategoryEntity>> ListWalCatsByGameId(Guid gameId);
    Task<int> Count();
    Task Create(WalletCategoryEntity walletCategory);
    Task Update(WalletCategoryEntity walletCategory);
    Task Delete(Guid categoryId);
}
