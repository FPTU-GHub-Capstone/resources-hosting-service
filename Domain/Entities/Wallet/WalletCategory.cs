using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Wallet;

[Table("WalletCategory")]
public class WalletCategory :BaseEntity
{
    public string Name { get; set; }
}
