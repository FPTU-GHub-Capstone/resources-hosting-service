using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.GameServices;

public class GameServerServices : IGameServerServices
{
    public readonly IGenericRepository<GameServer> _gameServerRepo;
    
    public GameServerServices(IGenericRepository<GameServer> gameServerRepo)
    {
        _gameServerRepo = gameServerRepo;
    }
    public async Task<ICollection<GameServer>> List()
    {
        var gameServer = await _gameServerRepo.ListAsync();
        return gameServer;
    }
    public async Task<GameServer> GetById(Guid gameServerId)
    {
        return await _gameServerRepo.FindByIdAsync(gameServerId);
    }
    public async Task<ICollection<GameServer>> GetByGameId(Guid gameId)
    {
        return await _gameServerRepo.WhereAsync(g => g.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _gameServerRepo.CountAsync();
    }
    public async Task Create(GameServer gameServer)
    {
        
    }
    public async Task Update(Guid gameServerId, GameServer gameServer)
    {

    }
    public async Task Delete(Guid gameServerId) { }
}
