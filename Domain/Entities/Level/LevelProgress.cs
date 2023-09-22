
using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Level;

[Table("LevelProgress")]
public class LevelProgress : BaseEntity
{
    public DateTime LevelUpDate { get; set; }
    public int ExpPoint { get; set; }
    public string Name { get; set; }
}
