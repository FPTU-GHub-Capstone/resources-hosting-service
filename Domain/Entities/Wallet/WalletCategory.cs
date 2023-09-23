using Domain.Common.BaseEntity;
using Domain.Entities.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Wallet;

[Table("WalletCategory")]
public class WalletCategory :BaseEntity
{
    public string Name { get; set; }
    //1 Game - M Wallet Category
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
}
