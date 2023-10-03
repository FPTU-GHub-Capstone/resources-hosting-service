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
        var games = await _gameRepo.ListAsync();
        return games;
    }
    public async Task<GameEntity> GetById(Guid gameId)
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
    public async Task<ICollection<GameEntity>> GetByUserId(Guid userId)
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
