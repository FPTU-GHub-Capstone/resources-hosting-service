using Domain.Common.BaseEntity;
using Domain.Entities.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Attribute;

[Table("AttributeGroup")]
public class AttributeGroup :BaseEntity
{
    public string Name { get; set; }
    public string Effect { get; set; } // JSON
    // M Attribute Group - M Game
    public virtual ICollection<GameEntity>? Games { get; set; }
}
