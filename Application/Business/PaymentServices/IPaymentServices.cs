using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IPaymentServices
{
    Task<ICollection<PaymentEntity>> List();
    Task<PaymentEntity> GetById(Guid paymentId);
    Task<ICollection<PaymentEntity>> GetByCharacterId(Guid id);
    Task<ICollection<PaymentEntity>> GetByUserId(Guid id);
    Task<ICollection<PaymentEntity>> GetByWalletId(Guid id);
    Task<int> Count();
    Task Create(PaymentEntity entity);
    Task Update(PaymentEntity entity);
    Task Delete(Guid paymentId);
}

