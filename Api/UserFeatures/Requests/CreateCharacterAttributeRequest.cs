using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateCharacterAttributeRequest : IMapTo<CharacterAttributeEntity>, IMapFrom<CharacterAttributeEntity>
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid AttributeGroupId { get; set; }
    }
}
