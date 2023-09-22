using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Asset;

[Table("Asset")]
public class AssetEntity : BaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
}
