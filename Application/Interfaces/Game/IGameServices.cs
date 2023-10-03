using Domain.Entities.Game;

namespace Application.Interfaces; 
public interface IGameServices {
    //Game
    Task<ICollection<GameEntity>> List();
    Task<GameEntity> GetById(Guid gameId);
    Task<ICollection<GameEntity>> GetByUserId(Guid userId);
    Task<int> Count();
    Task Create(GameEntity game);
    Task Update(Guid gameId, GameEntity game);
    Task Delete(Guid gameId);
}
