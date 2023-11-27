using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameServices
{
    Task<ICollection<GameEntity>> List();
    Task<GameEntity> GetById(Guid gameId);
    Task<ICollection<GameEntity>> List(Guid[] gameIds);
    Task Create(GameEntity game);
    Task Update(GameEntity game);
    Task Delete(Guid gameId);
}
