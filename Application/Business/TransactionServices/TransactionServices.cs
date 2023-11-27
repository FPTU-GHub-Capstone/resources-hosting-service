using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

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
        return await _transactionRepo.FoundOrThrowAsync(transactionId,
           Constants.ENTITY.TRANSACTION + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task Create(TransactionEntity transaction) {
        await _transactionRepo.CreateAsync(transaction);
    }
    public async Task Update(TransactionEntity transaction) {
        await _transactionRepo.UpdateAsync(transaction);
    }
    public async Task Delete(Guid transactionId) {
        await _transactionRepo.DeleteSoftAsync(transactionId);
    }
}
