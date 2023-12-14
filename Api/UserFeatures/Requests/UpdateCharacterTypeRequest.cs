using AutoWrapper.Filters;
using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateCharacterTypeRequest : IMapTo<CharacterTypeEntity>, IMapFrom<CharacterTypeEntity>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public JsonObject? BaseProperties { get; set; }
    }
}
