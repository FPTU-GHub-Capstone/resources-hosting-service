
using Domain.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User;

[Table("Client")]
public class Client :BaseEntity
{
    public string Name { get; set; }
    public string Scope { get;set; }
    public Guid ClientId { get; set; }
    public string ClientSecret { get; set; }
}
