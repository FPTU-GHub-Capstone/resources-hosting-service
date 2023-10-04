using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Asset")]
public class AssetEntity : BaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    // 1 Asset Type - M Asset
    public Guid AssetTypeId { get; set; }
    public AssetType AssetType { get; set; }
}
