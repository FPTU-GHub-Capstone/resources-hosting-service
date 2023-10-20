using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateWalletCategoryRequest : IMapFrom<WalletCategoryEntity>, IMapTo<WalletCategoryEntity>
    {
        public string? Name { get; set; }
    }
}
