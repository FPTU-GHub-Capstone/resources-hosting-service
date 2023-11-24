using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities;

[Table("GameUserEntity")]
public class GameUserEntity : BaseEntity
{
    public GameEntity Game { get; set; }
    public Guid GameId { get; set; }
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
}
