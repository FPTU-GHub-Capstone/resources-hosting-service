using Domain.Entities.Transaction;

namespace Application.Interfaces;
public interface ITransactionServices {
    //Transaction
    Task<ICollection<TransactionEntity>> List();
    Task<TransactionEntity> GetById(Guid transactionId);
    Task<ICollection<TransactionEntity>> GetByWalletId(Guid walletId);
    Task<int> Count();
    Task Create(TransactionEntity transaction);
    Task Update(Guid transactionId, TransactionEntity transaction);
    Task Delete(Guid transactionId);
}
