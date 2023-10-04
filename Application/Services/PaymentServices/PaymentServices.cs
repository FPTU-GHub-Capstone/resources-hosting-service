using Application.Interfaces;
using Domain.Entities.Payment;
using System.Collections.ObjectModel;

namespace Application.Services.PaymentServices;

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
    public async Task<int> Count()
    {
        return await _paymentRepo.CountAsync();
    }
    public async Task Create(PaymentEntity entity) { }
    public async Task Update(Guid paymentId, PaymentEntity entity) { }
    public async Task Delete(Guid paymentId) { }
}
