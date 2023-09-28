using Domain.Entities.Wallet;

namespace Application.Interfaces; 
public interface IWalletServices 
{
    //Wallet Category
    Task<ICollection<WalletCategory>> GetWalletCategories();
    Task<WalletCategory> GetWalletCategory(Guid categoryId);
    Task<ICollection<WalletCategory>> GetWalletCategories(Guid gameId);
    Task<int> CountWalletCategories();
    Task CreateWalletCategory(WalletCategory walletCategory);
    Task UpdateWalletCategory(Guid categoryId, WalletCategory walletCategory);
    Task DeleteWalletCategory(Guid categoryId);
    //Wallet Entity
    Task<ICollection<WalletEntity>> GetWallets();
    Task<WalletEntity> GetWallet(Guid walletId);
    Task<ICollection<WalletEntity>> GetWallets(Guid id, int typeId); //typeId: 1: WalletCategoryId, 2: CharacterId, 3: PaymentId
    Task<int> CountWallets();
    Task CreateWallet(WalletEntity wallet);
    Task UpdateWallet(Guid walletId, WalletEntity wallet);
    Task DeleteWallet(Guid walletId);
}
