using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Asset;

[Table("AssetAttribute")]
public class AssetAttribute : BaseEntity
{
    public int Value { get; set; }
}
