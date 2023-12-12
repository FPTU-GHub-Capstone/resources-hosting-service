using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ITransactionServices
{
    Task<ICollection<TransactionEntity>> List();
    Task<TransactionEntity> GetById(Guid transactionId);
    Task<ICollection<TransactionEntity>> ListTransactionsByGameId(Guid gameId);
    Task Create(TransactionEntity transaction);
    Task Update(TransactionEntity transaction);
    Task Delete(Guid transactionId);
}
