using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateWalletRequest : IMapFrom<WalletEntity>, IMapTo<WalletEntity>
    {
        public string? Name { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "Money cannot lower than 0")]
        public float? TotalMoney { get; set; }
    }
}
