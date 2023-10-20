using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Controllers;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateLevelProgressRequest : IMapFrom<LevelProgressEntity>, IMapTo<LevelProgressEntity>
    {
        [Required]
        public int ExpPoint { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid LevelId { get; set; }
    }
}
