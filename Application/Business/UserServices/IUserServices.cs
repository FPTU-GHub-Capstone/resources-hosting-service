using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IUserServices
{
    Task<ICollection<UserEntity>> List(string? email);
    Task<UserEntity> GetById(Guid UserId);
    Task<ICollection<UserEntity>> GetByGameId(Guid GameId);
    Task<int> Count();
    Task Create(UserEntity user);
    Task Update(UserEntity user);
    Task Delete(Guid UserId);
}
