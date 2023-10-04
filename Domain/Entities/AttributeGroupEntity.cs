using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("AttributeGroup")]
public class AttributeGroupEntity : BaseEntity
{
    public string Name { get; set; }
    public string Effect { get; set; } // JSON
    // M Attribute Group - M Game
    public virtual ICollection<GameEntity>? Games { get; set; }
}
