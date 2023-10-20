using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Payment")]
public class PaymentEntity : BaseEntity
{
    public int Amount { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public string BankCode { get; set; }
    public string Status { get; set; }
    //1 Character - M Payment
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
    //1 User - M Payment
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    //1 Wallet - 1 Payment
    public Guid WalletId { get; set; }
    public WalletEntity Wallet { get; set; }
}
