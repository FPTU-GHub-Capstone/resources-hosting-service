using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateAssetTypeRequest : IMapFrom<AssetTypeEntity>, IMapTo<AssetTypeEntity>
    {
        public string? Name { get; set; }
    }
}
