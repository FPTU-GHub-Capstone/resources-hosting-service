﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Game")]
public class GameEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Logo { get; set; }
    public string? Link { get; set; }
    public string? Banner { get; set; }
    public GamePlan GamePlan { get; set; }
    public bool IsActive { get; set; } = true; 

    // 1 Game - M Activity Type
    public virtual ICollection<ActivityTypeEntity>? ActivityTypes { get; set; }
    // M Game - M Attribute Group
    public virtual ICollection<AttributeGroupEntity>? AttributeGroups { get; set; }
}


public enum GamePlan
{
    Basic,
    Standard,
    Enterprise
}