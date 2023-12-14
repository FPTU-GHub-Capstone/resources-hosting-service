using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Entities;

[Table("Transaction")]
public class TransactionEntity : BaseEntity
{
    public string Name { get; set; }
    public float Value { get; set; }
    //1 Wallet - M Transaction
    public Guid WalletId { get; set; }
    public WalletEntity Wallet { get; set; }
    // 1 Transaction - 1 or M Activity
    [JsonIgnore]
    public virtual ICollection<ActivityEntity>? Activities { get; set; }
}
