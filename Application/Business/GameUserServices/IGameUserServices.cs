using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameUserServices
{
    Task<ICollection<GameUserEntity>> List();
    Task<GameUserEntity> GetById(Guid id);
    Task<List<UserEntity>> GetUserByGameId(Guid id);
    Task Create(GameUserEntity user);
    Task Delete(Guid id);
}
