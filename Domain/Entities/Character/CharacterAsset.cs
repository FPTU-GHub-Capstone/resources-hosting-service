
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
    public Guid AssetId { get; set; }
    public AssetEntity AssetEntity { get; set; }
    //1 Character - M CharacterAsset
    public Guid CharacterId { get; set; }
    public CharacterEntity CharacterEntity { get; set; }

}
