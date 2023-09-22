using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Transaction;

[Table("Transaction")]
public class TransactionEntity : BaseEntity
{
    public string Name { get; set; }
    public int Value { get; set; }
}
