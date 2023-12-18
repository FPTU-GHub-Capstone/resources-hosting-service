using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameServices
{
    Task<ICollection<GameEntity>> List();
    Task<GameEntity> GetById(Guid gameId);
    Task<ICollection<GameEntity>> List(Guid[] gameIds);
    Task<int> CountRecord(Guid id);
    Task ResetRecord(Guid[] gameIds);
    Task UpdateStatus(Guid[] gameIds);
    Task Create(GameEntity game);
    Task Update(GameEntity game);
    Task Delete(Guid gameId);
}
