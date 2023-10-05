using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Character")]
public class CharacterEntity : BaseEntity
{
    public string CurrentProperty { get; set; }
    public int PointReward { get; set; }
    //1 User - M Character
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    //1 CharacterType - M Character
    public Guid CharacterTypeId { get; set; }
    public CharacterTypeEntity CharacterType { get; set; }
    //1 GameServer - M Character
    public Guid GameServerId { get; set; }
    public GameServerEntity GameServer { get; set; }
    //1 Character - M CharacterAsset
    public virtual ICollection<CharacterAssetEntity>? CharacterAssets { get; set; }
}
