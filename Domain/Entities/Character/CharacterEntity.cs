using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

[Table("Character")]
public class CharacterEntity : BaseEntity
{
    public string CurrentProperty { get; set; }
    public int PointReward { get; set; }
}
