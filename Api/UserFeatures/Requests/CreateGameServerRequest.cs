using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateGameServerRequest : IMapTo<GameServerEntity>, IMapFrom<GameServerEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string ArtifactUrl { get; set; }
    }
}