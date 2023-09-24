using Domain.Common.BaseEntity;
using Domain.Entities.Character;
using Domain.Entities.Payment;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User;

[Table("User")]
public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get;set; }
    public string LastName { get;set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public float Balance { get; set; }
    // M User - M Client
    public virtual ICollection<Client> Clients { get; set; }
    // 1 User - M Characters
    public virtual ICollection<CharacterEntity> Characters { get; set; }
    //1 User - M Payment
    public virtual ICollection<PaymentEntity> Payments { get; set; }

}
