using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateWalletCategoryRequest : IMapFrom<WalletCategoryEntity>, IMapTo<WalletCategoryEntity>
    {
        [Required]
        public string Name { get; set; }
    }
}
