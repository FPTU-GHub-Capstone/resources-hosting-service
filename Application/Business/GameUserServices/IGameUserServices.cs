using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IGameUserServices
{
    Task<ICollection<GameUserEntity>> List();
    Task<GameUserEntity> GetById(Guid id);
    Task<ICollection<GameUserEntity>> GetByGameId(Guid id);
    Task<List<UserEntity>> GetUserByGameId(Guid id);
    Task<ICollection<GameUserEntity>> GetByUserId(Guid id);
    Task Create(GameUserEntity user);
    Task Delete(Guid id);
}
