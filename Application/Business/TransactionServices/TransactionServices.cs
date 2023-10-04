using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business.TransactionServices;

public class TransactionServices : ITransactionServices
{
    public readonly IGenericRepository<TransactionEntity> _transactionRepo;
    public TransactionServices(IGenericRepository<TransactionEntity> transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }
    public async Task<ICollection<TransactionEntity>> List()
    {
        return await _transactionRepo.ListAsync();
    }
    public async Task<TransactionEntity> GetById(Guid transactionId)
    {
        return await _transactionRepo.FindByIdAsync(transactionId);
    }
    public async Task<ICollection<TransactionEntity>> GetByWalletId(Guid walletId)
    {
        return await _transactionRepo.WhereAsync(t => t.WalletId.Equals(walletId));
    }
    public async Task<int> Count()
    {
        return await _transactionRepo.CountAsync();
    }
    public async Task Create(TransactionEntity transaction) { }
    public async Task Update(Guid transactionId, TransactionEntity transaction) { }
    public async Task Delete(Guid transactionId) { }
}
