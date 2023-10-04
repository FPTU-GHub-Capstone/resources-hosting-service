using Application.Interfaces;
using Domain.Entities.Game;

namespace Application.Services.GameServices;

public class GameServices : IGameServices
{
    public readonly IGenericRepository<GameEntity> _gameRepo;
    
    public GameServices(IGenericRepository<GameEntity> gameRepo)
    {
        _gameRepo = gameRepo;
    }

    //Game
    public async Task<ICollection<GameEntity>> List() {
        return await _gameRepo.ListAsync();
    }
    public async Task<GameEntity> GetById(Guid gameId)
    {
        return await _gameRepo.FindByIdAsync(gameId);
    }
    public async Task<ICollection<GameEntity>> GetByUserId(Guid userId)
    {
        return await _gameRepo.WhereAsync(g => g.UserId.Equals(userId));
    }
    public async Task<int> Count()
    {
        return await _gameRepo.CountAsync();
    }
    public async Task Create(GameEntity game)
    {

    }
    public async Task Update(Guid gameId, GameEntity game)
    {

    }
    public async Task Delete(Guid gameId)
    {

    }
}
