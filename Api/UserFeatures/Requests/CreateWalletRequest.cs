using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateWalletRequest : IMapFrom<WalletEntity>, IMapTo<WalletEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Money cannot lower than 0")]
        public float TotalMoney { get; set; }
        [Required]
        public Guid WalletCategoryId { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
    }
}
