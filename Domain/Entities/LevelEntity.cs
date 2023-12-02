using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Entities;

[Table("Level")]
public class LevelEntity : BaseEntity
{
    public string? Description { get; set; }
    public int LevelNo { get; set; }
    public int LevelUpPoint { get; set; }
    // 1 Game - M Levels
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    [JsonIgnore]
    // 1 Level Progress - 1 Level 
    public LevelProgressEntity LevelProgress { get; set; }
}
