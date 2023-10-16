using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateCharacterAttributeRequest : IMapTo<CharacterAttributeEntity>, IMapFrom<CharacterAttributeEntity>
    {
        public int? Value { get; set; }
    }
}
