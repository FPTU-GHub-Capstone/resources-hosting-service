using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateCharacterAssetRequest : IMapTo<CharacterAssetEntity>, IMapFrom<CharacterAssetEntity>
    {
        public int? Value { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
