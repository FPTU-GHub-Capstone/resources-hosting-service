using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests;

public class CreateAttributeGroupRequest : IMapTo<AttributeGroupEntity>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public JsonObject Effect { get; set; }
    [Required]
    public Guid GameId { get; set; }
}
