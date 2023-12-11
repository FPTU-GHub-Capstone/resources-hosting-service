using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateGameRequest : IMapTo<GameEntity>, IMapFrom<GameEntity>
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Link { get; set; }
        public string? Banner { get; set; }
    }
}