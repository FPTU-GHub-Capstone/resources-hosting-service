using Domain.Common.BaseEntity;
using Domain.Entities.Character;
using Domain.Entities.Payment;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Wallet;

[Table("Wallet")]
public class WalletEntity : BaseEntity
{
    public string Name { get; set; }
    public float TotalMoney { get; set; }
    // 1 Wallet Category - M Wallet
    public Guid WalletCategoryId { get; set; }
    public WalletCategory WalletCategory { get; set; }
    // 1 Character - M Wallet (ingame)
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
    // 1 Payment - 1 Wallet
    public Guid PaymentId { get; set; }
    public PaymentEntity Payment { get; set; }

}
