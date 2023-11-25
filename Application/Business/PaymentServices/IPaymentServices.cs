using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IPaymentServices
{
    Task<ICollection<PaymentEntity>> List();
    Task<ICollection<PaymentEntity>> ListPaymentByUserId(Guid id);
    Task Create(PaymentEntity entity);
    Task Update(PaymentEntity entity);
    Task Delete(Guid paymentId);
}

