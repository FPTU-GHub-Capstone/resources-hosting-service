using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateActivityTypeRequest : IMapTo<ActivityTypeEntity>, IMapFrom<ActivityTypeEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
    }
}
