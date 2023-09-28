using Application.Interfaces;
using Domain.Entities.Game;

namespace Application.Services.GameServices;

public class GameServices : IGameServices
{
    public readonly IGenericRepository<GameEntity> _gameRepo;
    public readonly IGenericRepository<GameServer> _gameServerRepo;
    
    public GameServices(IGenericRepository<GameEntity> gameRepo, IGenericRepository<GameServer> gameServerRepo)
    {
        _gameRepo = gameRepo;
        _gameServerRepo = gameServerRepo;
    }

    //Game
    public async Task<ICollection<GameEntity>> GetGames() {
        var games = await _gameRepo.ListAsync();
        return games;
    }
    public async Task<GameEntity> GetGame(Guid gameId)
    {
        var game = await _gameRepo.FindByIdAsync(gameId);
        if (game == null)
        {
            throw new Exception($"Game not exist");
        }
        else
        {
            return game;
        }
    }
    public async Task<ICollection<GameEntity>> GetGames(Guid userId)
    {
        var games = await _gameRepo.WhereAsync(
            g => g.UserId.Equals(userId));
        //Return if exist
        if (games.Count == 0)
        {
            throw new Exception($"Game or User not found");
        }
        else
        {
            return games;
        }
    }
    public async Task<int> CountGames()
    {
        var games = await _gameRepo.ListAsync();
        return games.Count;
    }
    public async Task CreateGame(GameEntity game)
    {

    }
    public async Task UpdateGame(Guid gameId, GameEntity game)
    {

    }
    public async Task DeleteGame(Guid gameId)
    {

    }
    //Game Server
    public async Task<ICollection<GameServer>> GetGameServers()
    {
        var gameServer = await _gameServerRepo.ListAsync();
        return gameServer;
    }
    public async Task<GameServer> GetGameServer(Guid gameServerId)
    {
        var gameServer = await _gameServerRepo.FindByIdAsync(gameServerId);
        if (gameServer == null)
        {
            throw new Exception($"Game server not exist");
        }
        else
        {
            return gameServer;
        }
    }
    public async Task<ICollection<GameServer>> GetGameServers(Guid gameId)
    {
        var gameServers = await _gameServerRepo.WhereAsync(
            g => g.GameId.Equals(gameId));
        //Return if exist
        if (gameServers.Count == 0)
        {
            throw new Exception($"Game Server or Game not found");
        }
        else
        {
            return gameServers;
        }
    }
    public async Task<int> CountGameServers()
    {
        var gameServer = await _gameServerRepo.ListAsync();
        return gameServer.Count;
    }
    public async Task CreateGameServer(GameServer gameServer)
    {
        
    }
    public async Task UpdateGameServer(Guid gameServerId, GameServer gameServer)
    {

    }
    public async Task DeleteGameServer(Guid gameServerId) { }
}
