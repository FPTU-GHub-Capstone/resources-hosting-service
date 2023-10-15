using AutoWrapper.Filters;
using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateCharacterTypeRequest : IMapTo<CharacterTypeEntity>, IMapFrom<CharacterTypeEntity>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? BaseProperties { get; set; }
    }
}
