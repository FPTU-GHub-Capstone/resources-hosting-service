using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameServerServices
{
    Task<ICollection<GameServerEntity>> List();
    Task<GameServerEntity> GetById(Guid gameServerId);
    Task<ICollection<GameServerEntity>> ListServersByGameId(Guid gameId);
    Task Create(GameServerEntity gameServer);
    Task Update(GameServerEntity gameServer);
    Task Delete(Guid gameServerId);
}
