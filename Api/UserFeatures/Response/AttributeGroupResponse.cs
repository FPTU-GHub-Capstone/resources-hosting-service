using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Response
{
    public class AttributeGroupResponse : IMapFrom<AttributeGroupEntity>,IMapTo<AttributeGroupEntity>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public JsonNode Effect { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
