using DomainLayer.Entities;
using WebApiLayer.Controllers;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateLevelProgressRequest : IMapFrom<LevelProgressEntity>, IMapTo<LevelProgressEntity>
    {
        public int? ExpPoint { get; set; }
        public string? Name { get; set; }
    }
}
