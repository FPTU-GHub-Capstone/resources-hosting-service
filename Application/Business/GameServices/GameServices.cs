using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class GameServices : IGameServices
{
    public readonly IGenericRepository<GameEntity> _gameRepo;

    public GameServices(IGenericRepository<GameEntity> gameRepo)
    {
        _gameRepo = gameRepo;
    }

    //Game
    public async Task<ICollection<GameEntity>> List()
    {
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
        var gameCheck = await _gameRepo.FirstOrDefaultAsync(
            g => g.Name.Equals(game.Name));
        if (gameCheck != null)
        {
            throw new BadRequestException("Name already exist");
        }
        await _gameRepo.CreateAsync(game);
    }
    public async Task Update(Guid gameId, GameEntity game)
    {
        await CheckGame(gameId);
        await _gameRepo.UpdateAsync(game);
    }
    public async Task Delete(Guid gameId)
    {
        await CheckGame(gameId);
        await _gameRepo.DeleteSoftAsync(gameId);
    }
    public async Task CheckGame(Guid id)
    {
        var target = await GetById(id);
        if (target is null)
        {
            throw new NotFoundException("Game not exist");
        }
    }
}