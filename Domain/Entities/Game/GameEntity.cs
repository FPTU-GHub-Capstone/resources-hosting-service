using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Game;

[Table("Game")]
public class GameEntity : BaseEntity
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Link { get; set; }
}
