using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

[Table("CharacterType")]
public class CharacterType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public string BaseProperties { get;set; } //JSON
}
