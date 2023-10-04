using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Client")]
public class Client : BaseEntity
{
    public string Name { get; set; }
    public string Scope { get; set; }
    public Guid ClientId { get; set; }
    public string ClientSecret { get; set; }
    // M Client - M User
    public virtual ICollection<UserEntity>? Users { get; set; }
}
