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
        var user = await _userRepo.ListAsync();
        return user;
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        var user = await _userRepo.FindByIdAsync(UserId);
        if (user == null)
        {
            throw new Exception($"User not exist");
        }
        else
        {
            return user;
        }
    }
    public async Task<int> Count()
    {
        return await _userRepo.CountAsync();
    }
    public async Task Create(UserEntity user) { }
    public async Task Update(Guid UserId, UserEntity user) { }
    public async Task Delete(Guid UserId) { }
}
