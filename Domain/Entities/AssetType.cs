using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("AssetType")]
public class AssetType : BaseEntity
{
    public string Name { get; set; }
    //1 Game - M Asset Type
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
}
