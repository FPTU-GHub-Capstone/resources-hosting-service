using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateAssetAttributeRequest : IMapFrom<AssetAttributeEntity>, IMapTo<AssetAttributeEntity>
    {
        public int? Value { get; set; }
    }
}
