using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateCharacterRequest : IMapTo<CharacterEntity>, IMapFrom<CharacterEntity>
    {
        [Required]
        public string CurrentProperty { get; set; }
        [Required]
        public int PointReward { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CharacterTypeId { get; set; }
        [Required]
        public Guid GameServerId { get; set; }
    }
}
