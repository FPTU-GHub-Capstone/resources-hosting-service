using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateActivityRequest : IMapTo<ActivityEntity>, IMapFrom<ActivityEntity>
    {
        public string? Name { get; set; }
        public int? Status { get; set; }
    }
}
