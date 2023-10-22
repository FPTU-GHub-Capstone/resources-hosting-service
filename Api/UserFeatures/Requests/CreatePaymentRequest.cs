using DomainLayer.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreatePaymentRequest : IMapTo<PaymentEntity>, IMapFrom<PaymentEntity>
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [Required]
        public string Content { get; set; }
        [Required]
        public string BankCode { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid WalletId { get; set; }

    }
}
