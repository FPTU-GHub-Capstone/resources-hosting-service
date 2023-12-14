using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Entities;

[Table("WalletCategory")]
public class WalletCategoryEntity : BaseEntity
{
    public string Name { get; set; }
    //1 Game - M Wallet Category
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    // 1 Wallet Category - M Wallet
    [JsonIgnore]
    public virtual ICollection<WalletEntity>? WalletEntities { get; set; }
}
