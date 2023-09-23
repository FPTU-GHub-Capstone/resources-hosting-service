using Domain.Common.BaseEntity;
using Domain.Entities.Wallet;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Transaction;

[Table("Transaction")]
public class TransactionEntity : BaseEntity
{
    public string Name { get; set; }
    public int Value { get; set; }
    //1 Wallet - M Transaction
    public Guid WalletId { get;set; }
    public WalletEntity Wallet { get; set; }
}
