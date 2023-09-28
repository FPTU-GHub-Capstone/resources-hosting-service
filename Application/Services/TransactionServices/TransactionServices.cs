using Application.Interfaces;
using Domain.Entities.Transaction;

namespace Application.Services.TransactionServices;

public class TransactionServices : ITransactionServices
{
    public readonly IGenericRepository<TransactionEntity> _transactionRepo;
    public TransactionServices(IGenericRepository<TransactionEntity> transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }
    //Transaction
    public async Task<ICollection<TransactionEntity>> GetTransactions()
    {
        var trans = await _transactionRepo.ListAsync();
        return trans;
    }
    public async Task<TransactionEntity> GetTransaction(Guid transactionId)
    {
        var trans = await _transactionRepo.FindByIdAsync(transactionId);
        if (trans == null)
        {
            throw new Exception($"Transaction not exist");
        }
        else
        {
            return trans;
        }
    }
    public async Task<ICollection<TransactionEntity>> GetTransactions(Guid walletId)
    {
        var trans = await _transactionRepo.WhereAsync(
            t => t.WalletId.Equals(walletId));
        if (trans.Count == 0 || trans == null)
        {
            throw new Exception($"Transaction or wallet not found");
        }
        else
        {
            return trans;
        }
    }
    public async Task<int> CountTransaction()
    {
        var trans = await _transactionRepo.ListAsync();
        return trans.Count;
    }
    public async Task CreateTransaction(TransactionEntity transaction) { }
    public async Task UpdateTransaction(Guid transactionId, TransactionEntity transaction) { }
    public async Task DeleteTransaction(Guid transactionId) { }
}
