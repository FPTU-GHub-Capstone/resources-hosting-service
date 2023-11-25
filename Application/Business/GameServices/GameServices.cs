using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Reflection.Metadata;

namespace ServiceLayer.Business;

public class GameServices : IGameServices
{
    public readonly IGenericRepository<GameEntity> _gameRepo;

    public GameServices(IGenericRepository<GameEntity> gameRepo)
    {
        _gameRepo = gameRepo;
    }
    public async Task<ICollection<GameEntity>> List()
    {
        return await _gameRepo.ListAsync();
    }
    public async Task<GameEntity> GetById(Guid gameId)
    {
        return await _gameRepo.FoundOrThrowAsync(gameId,
            Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<GameEntity>> List(Guid[] gameIds)
    {
        List<GameEntity> result = new List<GameEntity>();
        foreach(var gameId in gameIds.Distinct())
        {
            var game = await _gameRepo.FindByIdAsync(gameId);
            if (game is not null)
            {
                result.Add(game);
            }
            else
            {
                throw new NotFoundException("Game with ID " + gameId + " " + Constants.ERROR.NOT_EXIST_ERROR);
            }
        }
        return result;
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
    public async Task Update(GameEntity game)
    {
        await _gameRepo.UpdateAsync(game);
    }
    public async Task Delete(Guid gameId)
    {
        await _gameRepo.DeleteSoftAsync(gameId);
    }
}