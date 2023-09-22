

using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Asset;

[Table("AssetType")]
public class AssetType : BaseEntity
{
    public string Name { get; set; }
}
