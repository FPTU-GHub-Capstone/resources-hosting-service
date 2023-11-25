using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IPaymentServices
{
    Task<ICollection<PaymentEntity>> List();
    Task<PaymentEntity> GetById(Guid paymentId);
    Task<ICollection<PaymentEntity>> ListPaymentByCharId(Guid id);
    Task<ICollection<PaymentEntity>> ListPaymentByUserId(Guid id);
    Task<ICollection<PaymentEntity>> ListPaymentByWalletId(Guid id);
    Task<int> Count();
    Task Create(PaymentEntity entity);
    Task Update(PaymentEntity entity);
    Task Delete(Guid paymentId);
}

