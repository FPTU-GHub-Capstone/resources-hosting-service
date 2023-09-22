
using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Game;

[Table("GameServer")]
public class GameServer : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string ArtifactUrl { get; set; }
}
