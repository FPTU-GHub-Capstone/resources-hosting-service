using Domain.Entities.Game;

namespace Application.Interfaces; 
public interface IGameServices {
    //Game
    Task<ICollection<GameEntity>> GetGames();
    Task<GameEntity> GetGame(Guid gameId);
    Task<ICollection<GameEntity>> GetGames(Guid userId);
    Task<int> CountGames();
    Task CreateGame (GameEntity game);
    Task UpdateGame (Guid gameId, GameEntity game);
    Task DeleteGame (Guid gameId);
    //Game Server
    Task <ICollection<GameServer>> GetGameServers();
    Task <GameServer> GetGameServer(Guid gameServerId);
    Task<ICollection<GameServer>> GetGameServers(Guid gameId);
    Task<int> CountGameServers();
    Task CreateGameServer(GameServer gameServer);
    Task UpdateGameServer(Guid gameServerId, GameServer gameServer);
    Task DeleteGameServer(Guid gameServerId);
}
