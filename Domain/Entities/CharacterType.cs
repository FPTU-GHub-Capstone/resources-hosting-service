using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("CharacterType")]
public class CharacterType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BaseProperties { get; set; } //JSON
    // 1 Game - M Character Type
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
}
