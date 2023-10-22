using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateTransactionRequest : IMapTo<TransactionEntity>, IMapFrom<TransactionEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float Value { get; set; }
        [Required]
        public Guid WalletId { get; set; }
    }
}
