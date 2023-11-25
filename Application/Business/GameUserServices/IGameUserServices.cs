using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameUserServices
{
    Task<ICollection<GameUserEntity>> List();
    Task<List<UserEntity>> ListUsersByGameId(Guid id);
    Task<List<GameEntity>> ListGamesByUserId(Guid id);
    Task Create(GameUserEntity user);
    Task Delete(Guid id);
}
