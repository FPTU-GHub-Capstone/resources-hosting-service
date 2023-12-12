using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("AttributeGroup")]
public class AttributeGroupEntity : BaseEntity
{
    public string Name { get; set; }
    public string Effect { get; set; } // JSON
    // M Attribute Group - 1 Game
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
}
