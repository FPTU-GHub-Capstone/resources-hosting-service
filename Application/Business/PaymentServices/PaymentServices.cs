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
    public async Task<ICollection<PaymentEntity>> ListPaymentByUserId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.UserId == id);
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
