using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IWalletServices
{
    Task<ICollection<WalletEntity>> List();
    Task<WalletEntity> GetById(Guid walletId);
    Task<ICollection<WalletEntity>> ListWalletsByCharacterId(Guid charId);
    Task<ICollection<WalletEntity>> ListWalletsByGameId(Guid gameId);
    Task Create(WalletEntity wallet);
    Task Update(WalletEntity wallet);
    Task Delete(Guid walletId);
}
