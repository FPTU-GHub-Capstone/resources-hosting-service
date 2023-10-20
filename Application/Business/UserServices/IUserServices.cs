using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface IUserServices
{
    Task<ICollection<UserEntity>> List();
    Task<UserEntity> GetById(Guid UserId);
    Task<int> Count();
    Task Create(UserEntity user);
    Task Update(UserEntity user);
    Task Delete(Guid UserId);
}
