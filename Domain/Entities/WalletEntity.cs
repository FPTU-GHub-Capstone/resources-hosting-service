using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Wallet")]
public class WalletEntity : BaseEntity
{
    public string Name { get; set; }
    public float TotalMoney { get; set; }
    // 1 Wallet Category - M Wallet
    public Guid WalletCategoryId { get; set; }
    public WalletCategoryEntity WalletCategory { get; set; }
    // 1 Character - M Wallet (ingame)
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
    // 1 Payment - 1 Wallet
    public PaymentEntity Payment { get; set; }

}
