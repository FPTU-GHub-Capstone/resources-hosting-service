using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class PaymentServices : IPaymentServices
{
    public readonly IGenericRepository<PaymentEntity> _paymentRepo;
    
    public PaymentServices(IGenericRepository<PaymentEntity> paymentRepo)
    {
        _paymentRepo = paymentRepo;
    }
    public async Task<ICollection<PaymentEntity>> List()
    {
        return await _paymentRepo.ListAsync();
    }
    public async Task<PaymentEntity> GetById(Guid paymentId)
    {
        return await _paymentRepo.FoundOrThrowAsync(paymentId,
            Constants.ENTITY.PAYMENT + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<PaymentEntity>> ListPaymentByCharId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c=>c.CharacterId== id);
    }
    public async Task<ICollection<PaymentEntity>> ListPaymentByUserId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.UserId == id);
    }
    public async Task<ICollection<PaymentEntity>> ListPaymentByWalletId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.WalletId == id);
    }
    public async Task<int> Count()
    {
        return await _paymentRepo.CountAsync();
    }
    public async Task Create(PaymentEntity entity) {
        var gameCheck = await _paymentRepo.FirstOrDefaultAsync(
            g => g.WalletId.Equals(entity.WalletId));
        if (gameCheck != null)
        {
            throw new BadRequestException("Wallet already exist in another payment");
        }
        await _paymentRepo.CreateAsync(entity);
    }
    public async Task Update(PaymentEntity entity) {
        await _paymentRepo.UpdateAsync(entity);
    }
    public async Task Delete(Guid paymentId) {
        await _paymentRepo.DeleteSoftAsync(paymentId);
    }
}
