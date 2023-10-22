using DomainLayer.Entities;
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
        return await _paymentRepo.FindByIdAsync(paymentId);
    }
    public async Task<ICollection<PaymentEntity>> GetByCharacterId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c=>c.CharacterId== id);
    }
    public async Task<ICollection<PaymentEntity>> GetByUserId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.UserId == id);
    }
    public async Task<ICollection<PaymentEntity>> GetByWalletId(Guid id)
    {
        return await _paymentRepo.WhereAsync(c => c.WalletId == id);
    }
    public async Task<int> Count()
    {
        return await _paymentRepo.CountAsync();
    }
    public async Task Create(PaymentEntity entity) {
        await _paymentRepo.CreateAsync(entity);
    }
    public async Task Update(PaymentEntity entity) {
        await _paymentRepo.UpdateAsync(entity);
    }
    public async Task Delete(Guid paymentId) {
        await _paymentRepo.DeleteSoftAsync(paymentId);
    }
}
