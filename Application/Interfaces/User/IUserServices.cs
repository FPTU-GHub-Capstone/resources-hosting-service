using Domain.Entities.User;

namespace Application.Interfaces; 
public interface IUserServices {
    Task<ICollection<UserEntity>> List();
    Task<UserEntity> GetById(Guid UserId);
    Task<int> Count();
    Task Create(UserEntity user);
    Task Update(Guid UserId, UserEntity user);
    Task Delete(Guid UserId);
}
