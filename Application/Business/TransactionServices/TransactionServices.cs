using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class TransactionServices : ITransactionServices
{
    private readonly IGenericRepository<TransactionEntity> _transactionRepo;
    private readonly IWalletServices _walletServices;
    public TransactionServices(IGenericRepository<TransactionEntity> transactionRepo, IWalletServices walletServices)
    {
        _transactionRepo = transactionRepo;
        _walletServices = walletServices;
    }
    public async Task<ICollection<TransactionEntity>> List()
    {
        return await _transactionRepo.ListAsync();
    }
    public async Task<TransactionEntity> GetById(Guid transactionId)
    {
        return await _transactionRepo.FoundOrThrowAsync(transactionId,
           Constants.Entities.TRANSACTION + Constants.Errors.NOT_EXIST_ERROR);
    }

    public async Task<ICollection<TransactionEntity>> ListTransactionsByGameId(Guid gameId)
    {
        var walletIds = (await _walletServices.ListWalletsByGameId(gameId)).Select(x => x.Id);
        return await _transactionRepo.WhereAsync(x => walletIds.Contains(x.WalletId));
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
