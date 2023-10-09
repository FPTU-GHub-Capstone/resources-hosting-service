using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests;

public class UpdateAttributeGroupRequest : IMapTo<AttributeGroupEntity>,IMapFrom<AttributeGroupEntity>
{
    public string? Name { get; set; }
    public string? Effect { get; set; }
}
