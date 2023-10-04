using Domain.Entities;

namespace Application.Interfaces;
public interface IGameServerServices {
    Task <ICollection<GameServer>> List();
    Task <GameServer> GetById(Guid gameServerId);
    Task<ICollection<GameServer>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(GameServer gameServer);
    Task Update(Guid gameServerId, GameServer gameServer);
    Task Delete(Guid gameServerId);
}
