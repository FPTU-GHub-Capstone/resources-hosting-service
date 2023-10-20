using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateCharacterAssetRequest : IMapTo<CharacterAssetEntity>, IMapFrom<CharacterAssetEntity>
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        [Required]
        public Guid AssetsId { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
    }
}
