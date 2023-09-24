using Domain.Common.BaseEntity;
using Domain.Entities.Game;
using Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Character;

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
    public CharacterType CharacterType { get; set; }
    //1 GameServer - M Character
    public Guid GameServerId { get; set; }
    public GameServer GameServer { get; set; }
    //1 Character - M CharacterAsset
    public virtual ICollection<CharacterAsset> CharacterAssets { get;set; }
}
