using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Attribute;

[Table("AttributeGroup")]
public class AttributeGroup :BaseEntity
{
    public string Name { get; set; }
    public string Effect { get; set; } // JSON
}
