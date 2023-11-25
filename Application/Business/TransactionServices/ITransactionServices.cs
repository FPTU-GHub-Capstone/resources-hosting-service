using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ITransactionServices
{
    Task<ICollection<TransactionEntity>> List();
    Task<TransactionEntity> GetById(Guid transactionId);
    Task Create(TransactionEntity transaction);
    Task Update(TransactionEntity transaction);
    Task Delete(Guid transactionId);
}
