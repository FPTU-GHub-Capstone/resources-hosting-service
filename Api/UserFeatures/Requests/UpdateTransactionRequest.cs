using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateTransactionRequest : IMapTo<TransactionEntity>, IMapFrom<TransactionEntity>
    {
        public string? Name { get; set; }
        public float? Value { get; set; }
    }
}
