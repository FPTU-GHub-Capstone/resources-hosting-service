using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Transaction")]
public class TransactionEntity : BaseEntity
{
    public string Name { get; set; }
    public int Value { get; set; }
    //1 Wallet - M Transaction
    public Guid WalletId { get; set; }
    public WalletEntity Wallet { get; set; }
    // 1 Transaction - 1 or M Activity
    public virtual ICollection<ActivityEntity>? Activities { get; set; }
}
