using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameServerServices
{
    Task<ICollection<GameServerEntity>> List();
    Task<GameServerEntity> GetById(Guid gameServerId);
    Task<ICollection<GameServerEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(GameServerEntity gameServer);
    Task Update(GameServerEntity gameServer);
    Task Delete(Guid gameServerId);
}
