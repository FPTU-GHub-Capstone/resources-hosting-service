
using Domain.Common.BaseEntity;
using Domain.Entities.Asset;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

[Table("CharacterAsset")]
public class CharacterAsset : BaseEntity
{
    public int Value { get; set; }
    public DateTime ExpiredDate { get; set; }
    //1 Asset - M CharacterAsset
    public Guid AssetsId { get; set; }
    public AssetEntity Assets { get; set; }
    //1 Character - M CharacterAsset
    public Guid CharacterId { get; set; }
    public CharacterEntity Characters { get; set; }

}
