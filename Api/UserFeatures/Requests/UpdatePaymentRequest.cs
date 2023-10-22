using DomainLayer.Entities;
using System.ComponentModel;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdatePaymentRequest : IMapTo<PaymentEntity>, IMapFrom<PaymentEntity>
    {
        public int? Amount { get; set; }
        public string? Content { get; set; }
        public string? BankCode { get; set; }
        public string? Status { get; set; }
    }
}
