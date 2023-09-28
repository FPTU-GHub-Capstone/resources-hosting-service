using Domain.Entities.Payment;

namespace Application.Interfaces;
public interface IPaymentServices {
    //Payment
    Task<ICollection<PaymentEntity>> GetPayments();
    Task<PaymentEntity> GetPayment(Guid paymentId);
    Task<ICollection<PaymentEntity>> GetPayments(Guid id, int typeId); // typeId: 1: CharacterId, 2: UserId
    Task<int> CountPayments();
    Task CreatePayment (PaymentEntity entity);
    Task UpdatePayment (Guid paymentId, PaymentEntity entity);
    Task DeletePayment (Guid paymentId);
}

