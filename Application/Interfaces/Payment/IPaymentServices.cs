using Domain.Entities.Payment;

namespace Application.Interfaces;
public interface IPaymentServices {
    //Payment
    Task<ICollection<PaymentEntity>> List();
    Task<PaymentEntity> GetById(Guid paymentId);
    Task<ICollection<PaymentEntity>> GetById(Guid id, int typeId); // typeId: 1: CharacterId, 2: UserId
    Task<int> Count();
    Task Create(PaymentEntity entity);
    Task Update(Guid paymentId, PaymentEntity entity);
    Task Delete(Guid paymentId);
}

