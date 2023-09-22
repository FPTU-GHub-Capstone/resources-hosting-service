using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Wallet;

[Table("Wallet")]
public class WalletEntity : BaseEntity
{
    public string Name { get; set; }
    public float TotalMoney { get; set; }
}
