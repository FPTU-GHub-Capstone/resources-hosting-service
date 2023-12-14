using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests;

public class UpdateAttributeGroupRequest : IMapTo<AttributeGroupEntity>,IMapFrom<AttributeGroupEntity>
{
    public string? Name { get; set; }
    public JsonObject? Effect { get; set; }
}
