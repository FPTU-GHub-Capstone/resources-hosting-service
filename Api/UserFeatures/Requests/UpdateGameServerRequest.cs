using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class UpdateGameServerRequest : IMapTo<GameServerEntity>, IMapFrom<GameServerEntity>
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? ArtifactUrl { get; set; }
    }
}