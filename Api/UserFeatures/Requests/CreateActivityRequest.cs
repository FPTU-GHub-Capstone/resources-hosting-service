using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateActivityRequest : IMapTo<ActivityEntity>, IMapFrom<ActivityEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public Guid ActivityTypeId { get; set; }
        [Required]
        public Guid TransactionId { get; set; }
    }
}
