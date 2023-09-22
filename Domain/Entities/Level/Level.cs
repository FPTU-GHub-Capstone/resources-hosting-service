using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Level;

[Table("Level")]
public class Level : BaseEntity
{
    public string Name { get; set; }
    public int LevelUpPoint { get; set; }
}
