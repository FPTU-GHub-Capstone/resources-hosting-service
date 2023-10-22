using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateAssetAttributeRequest : IMapFrom<AssetAttributeEntity>, IMapTo<AssetAttributeEntity>
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public Guid AssetId { get; set; }
        [Required]
        public Guid AttributeGroupId { get; set; }
    }
}
