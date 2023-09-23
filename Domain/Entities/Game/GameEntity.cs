using Domain.Common.BaseEntity;
using Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Game;

[Table("Game")]
public class GameEntity : BaseEntity
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Link { get; set; }
    // 1 User - M Games
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}
