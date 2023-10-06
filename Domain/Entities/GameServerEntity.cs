﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("GameServer")]
public class GameServerEntity : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string ArtifactUrl { get; set; }
    // 1 Game - M Game Server
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    //1 GameServer - M Character
    public virtual ICollection<CharacterEntity>? Characters { get; set; }
}