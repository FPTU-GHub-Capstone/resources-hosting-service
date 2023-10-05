using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("AssetAttribute")]
public class AssetAttributeEntity : BaseEntity
{
    public int Value { get; set; }
    //1 Asset - M Asset Attribute
    public Guid AssetId { get; set; }
    public AssetEntity Asset { get; set; }
    //1 Attribute Group - M Asset Attribute
    public Guid AttributeGroupId { get; set; }
    public AttributeGroupEntity AttributeGroup { get; set; }
}
