using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateAssetRequest : IMapTo<AssetEntity>, IMapFrom<AssetEntity>
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}
