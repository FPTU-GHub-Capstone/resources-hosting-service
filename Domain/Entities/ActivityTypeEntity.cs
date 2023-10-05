using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("ActivityType")]
public class ActivityTypeEntity : BaseEntity
{
    public string Name { get; set; }
    // 1 Game - M Activity Type
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    // 1 Character - M Activity Type
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
}
