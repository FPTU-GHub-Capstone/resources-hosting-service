using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Asset")]
public class AssetEntity : BaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    // 1 Asset Type - M Asset
    public Guid AssetTypeId { get; set; }
    public AssetTypeEntity AssetType { get; set; }
}
