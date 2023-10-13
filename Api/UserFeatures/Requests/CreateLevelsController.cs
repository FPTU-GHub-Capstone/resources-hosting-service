using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateLevelsController : IMapTo<LevelEntity>, IMapFrom<LevelEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int LevelUpPoint { get; set; }
        [Required]
        public Guid GameId { get; set; }
    }
}