using DomainLayer.Entities;

namespace ServiceLayer.Business.GameServer;
public interface IGameServerServices
{
    Task<ICollection<GameServerEntity>> List();
    Task<GameServerEntity> GetById(Guid gameServerId);
    Task<ICollection<GameServerEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(GameServerEntity gameServer);
    Task Update(Guid gameServerId, GameServerEntity gameServer);
    Task Delete(Guid gameServerId);
}
