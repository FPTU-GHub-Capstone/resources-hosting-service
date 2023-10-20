using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateActivityTypeRequest : IMapTo<ActivityTypeEntity>, IMapFrom<ActivityTypeEntity>
    {
        public string? Name { get; set; }
    }
}
