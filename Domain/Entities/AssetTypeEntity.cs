using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("AssetType")]
public class AssetTypeEntity : BaseEntity
{
    public string Name { get; set; }
    //1 Game - M Asset Type
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
}
