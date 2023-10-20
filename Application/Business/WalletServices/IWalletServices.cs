using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IWalletServices
{
    Task<ICollection<WalletEntity>> List();
    Task<WalletEntity> GetById(Guid walletId);
    Task<ICollection<WalletEntity>> GetByWalletCategoryId(Guid id);
    Task<ICollection<WalletEntity>> GetByCharacterId(Guid id);
    Task<int> Count();
    Task Create(WalletEntity wallet);
    Task Update(WalletEntity wallet);
    Task Delete(Guid walletId);
}
