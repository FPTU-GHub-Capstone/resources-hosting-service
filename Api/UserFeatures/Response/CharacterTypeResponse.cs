using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Response
{
    public class CharacterTypeResponse : IMapFrom<CharacterTypeEntity>,IMapTo<CharacterTypeEntity>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public JsonNode BaseProperties { get; set; } //JSON
        public Guid GameId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
