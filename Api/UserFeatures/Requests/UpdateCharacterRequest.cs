using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateCharacterRequest : IMapTo<CharacterEntity>, IMapFrom<CharacterEntity>
    {
        public string? CurrentProperty { get; set; }
        public int? PointReward { get; set; }
    }
}
