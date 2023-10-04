using Domain.Entities.Wallet;

namespace Application.Interfaces; 
public interface IWalletServices 
{
    Task<ICollection<WalletEntity>> List();
    Task<WalletEntity> GetById(Guid walletId);
    Task<ICollection<WalletEntity>> GetByWalletCategoryId(Guid id); //typeId: 1: WalletCategoryId, 2: CharacterId, 3: PaymentId
    Task<ICollection<WalletEntity>> GetByCharacterId(Guid id);
    Task<ICollection<WalletEntity>> GetByPaymentId(Guid id);
    Task<int> Count();
    Task Create(WalletEntity wallet);
    Task Update(Guid walletId, WalletEntity wallet);
    Task Delete(Guid walletId);
}
