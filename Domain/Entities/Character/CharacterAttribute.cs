using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

[Table("CharacterAttribute")]
public class CharacterAttribute : BaseEntity
{
    public int Value { get; set; }
}
