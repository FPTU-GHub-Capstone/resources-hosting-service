using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("AssetAttribute")]
public class AssetAttribute : BaseEntity
{
    public int Value { get; set; }
    //1 Asset - M Asset Attribute
    public Guid AssetId { get; set; }
    public AssetEntity Asset { get; set; }
    //1 Attribute Group - M Asset Attribute
    public Guid AttributeGroupId { get; set; }
    public AttributeGroup AttributeGroup { get; set; }
}
