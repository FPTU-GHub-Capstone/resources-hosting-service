
using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

[Table("CharacterAsset")]
public class CharacterAsset : BaseEntity
{
    public int Value { get; set; }
    public DateTime ExpiredDate { get; set; }
}
