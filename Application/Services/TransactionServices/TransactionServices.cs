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
    public async Task<ICollection<TransactionEntity>> List()
    {
        var trans = await _transactionRepo.ListAsync();
        return trans;
    }
    public async Task<TransactionEntity> GetById(Guid transactionId)
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
    public async Task<ICollection<TransactionEntity>> GetByWalletId(Guid walletId)
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
    public async Task<int> Count()
    {
        return await _transactionRepo.CountAsync();
    }
    public async Task Create(TransactionEntity transaction) { }
    public async Task Update(Guid transactionId, TransactionEntity transaction) { }
    public async Task Delete(Guid transactionId) { }
}
