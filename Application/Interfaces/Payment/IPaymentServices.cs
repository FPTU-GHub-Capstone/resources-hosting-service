using Domain.Entities.Payment;

namespace Application.Interfaces;
public interface IPaymentServices {
    Task<ICollection<PaymentEntity>> List();
    Task<PaymentEntity> GetById(Guid paymentId);
    Task<ICollection<PaymentEntity>> GetByCharacterId(Guid id);
    Task<ICollection<PaymentEntity>> GetByUserId(Guid id);
    Task<int> Count();
    Task Create(PaymentEntity entity);
    Task Update(Guid paymentId, PaymentEntity entity);
    Task Delete(Guid paymentId);
}

