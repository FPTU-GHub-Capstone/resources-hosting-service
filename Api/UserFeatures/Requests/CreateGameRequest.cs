using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateGameRequest : IMapTo<GameEntity>, IMapFrom<GameEntity>
    {
        [Required]
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Link { get; set; }
        public string? Banner { get; set; }
        [Required]
        [Range(0, 2)]
        public GamePlan GamePlan { get; set; }
    }
}
