﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("User")]
public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; } = "";
    public int Status { get; set; } = 0;
    public float Balance { get; set; } = 0;
    // M User - M Games
    public virtual ICollection<GameEntity>? Games { get; set; }
    // 1 User - M Characters
    public virtual ICollection<CharacterEntity>? Characters { get; set; }
    //1 User - M Payment
    public virtual ICollection<PaymentEntity>? Payments { get; set; }
    // M User - M Games
    public virtual ICollection<GameEntity>? Games { get; set; }

}
