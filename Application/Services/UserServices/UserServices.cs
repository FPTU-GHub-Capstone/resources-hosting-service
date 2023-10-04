using Application.Interfaces;
using Domain.Entities.Attribute;
using Domain.Entities.User;

namespace Application.Services.UserServices;

public class UserServices : IUserServices
{
    public readonly IGenericRepository<UserEntity> _userRepo;

    public UserServices(IGenericRepository<UserEntity> userRepo)
    {
        _userRepo = userRepo;
    }
    public async Task<ICollection<UserEntity>> List()
    {
        return await _userRepo.ListAsync();
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        return await _userRepo.FindByIdAsync(UserId);
    }
    public async Task<int> Count()
    {
        return await _userRepo.CountAsync();
    }
    public async Task Create(UserEntity user) { }
    public async Task Update(Guid UserId, UserEntity user) { }
    public async Task Delete(Guid UserId) { }
}
