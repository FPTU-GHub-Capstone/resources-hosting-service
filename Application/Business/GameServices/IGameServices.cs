using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameServices
{
    Task<ICollection<GameEntity>> List();
    Task<GameEntity> GetById(Guid gameId);
    Task<ICollection<GameEntity>> List(Guid[] gameIds);
    Task<ICollection<GameEntity>> GetByUserId(Guid userId);
    Task<int> Count();
    Task Create(GameEntity game);
    Task Update(GameEntity game);
    Task Delete(Guid gameId);
}
