
using Domain.Common.BaseEntity;
using Domain.Entities.Character;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Level;

[Table("LevelProgress")]
public class LevelProgress : BaseEntity
{
    public DateTime LevelUpDate { get; set; }
    public int ExpPoint { get; set; }
    public string Name { get; set; }
    //1 Character - M Level Progress
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
    //1 Level - 1 Level Progress
    public Guid LevelId { get; set; }
    public LevelEntity Level { get; set; }

}
