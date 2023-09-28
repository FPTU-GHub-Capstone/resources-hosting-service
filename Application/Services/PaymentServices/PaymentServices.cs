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
    //Payment
    public async Task<ICollection<PaymentEntity>> GetPayments()
    {
        var pm = await _paymentRepo.ListAsync();
        return pm;
    }
    public async Task<PaymentEntity> GetPayment(Guid paymentId)
    {
        var pm = await _paymentRepo.FindByIdAsync(paymentId);
        return pm;
    }
    public async Task<ICollection<PaymentEntity>> GetPayments(Guid id, int typeId)
    { // typeId: 1: CharacterId, 2: UserId
        ICollection<PaymentEntity> payments = new Collection<PaymentEntity>();
        if (typeId == 1)
        {
            payments = await _paymentRepo.WhereAsync(
                a => a.CharacterId.Equals(id));
        }
        else if (typeId == 2)
        {
            payments = await _paymentRepo.WhereAsync(
                a => a.UserId.Equals(id));
        }
        //Return if exist
        if (payments.Count == 0 || payments == null)
        {
            throw new Exception($"Payment or Character/User not found");
        }
        else
        {
            return payments;
        }
    }
    public async Task<int> CountPayments()
    {
        var pm = await _paymentRepo.ListAsync();
        return pm.Count;
    }
    public async Task CreatePayment(PaymentEntity entity) { }
    public async Task UpdatePayment(Guid paymentId, PaymentEntity entity) { }
    public async Task DeletePayment(Guid paymentId) { }
}
