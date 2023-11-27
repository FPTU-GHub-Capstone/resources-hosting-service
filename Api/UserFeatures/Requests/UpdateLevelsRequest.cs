using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateLevelsRequest : IMapTo<LevelEntity>, IMapFrom<LevelEntity>
    {
        public string? Description { get; set; }
        public int? LevelUpPoint { get; set; }
    }
}