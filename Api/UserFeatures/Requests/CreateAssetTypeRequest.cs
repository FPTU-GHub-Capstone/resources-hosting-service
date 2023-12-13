using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateAssetTypeRequest : IMapFrom<AssetTypeEntity>, IMapTo<AssetTypeEntity>
    {
        [Required]
        public string Name { get; set; }
    }
}
