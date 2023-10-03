using Domain.Entities.Wallet;

namespace Application.Interfaces; 
public interface IWalletServices 
{
    //Wallet Entity
    Task<ICollection<WalletEntity>> List();
    Task<WalletEntity> GetById(Guid walletId);
    Task<ICollection<WalletEntity>> GetById(Guid id, int typeId); //typeId: 1: WalletCategoryId, 2: CharacterId, 3: PaymentId
    Task<int> Count();
    Task Create(WalletEntity wallet);
    Task Update(Guid walletId, WalletEntity wallet);
    Task Delete(Guid walletId);
}
