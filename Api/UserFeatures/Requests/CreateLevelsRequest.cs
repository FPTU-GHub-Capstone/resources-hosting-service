using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateLevelsRequest : IMapTo<LevelEntity>, IMapFrom<LevelEntity>
    {
        public string? Description { get; set; }
        [Required]
        public int LevelUpPoint { get; set; }
        [Required]
        public Guid GameId { get; set; }
    }
}