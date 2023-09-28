using Domain.Entities.Payment;
using Domain.Entities.Transaction;

namespace Application.Interfaces;
public interface ITransactionServices {
    //Transaction
    Task<ICollection<TransactionEntity>> GetTransactions();
    Task<TransactionEntity> GetTransaction(Guid transactionId);
    Task<ICollection<TransactionEntity>> GetTransactions(Guid walletId);
    Task<int> CountTransaction();
    Task CreateTransaction(TransactionEntity transaction);
    Task UpdateTransaction(Guid transactionId, TransactionEntity transaction);
    Task DeleteTransaction(Guid transactionId);
}
