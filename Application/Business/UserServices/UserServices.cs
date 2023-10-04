using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business.UserServices;

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
    public async Task Create(UserEntity user) {
        var userList = List();
        var check = false;
        foreach(var User in userList.Result)
        {
            if(User.Email == user.Email)
            {
                check = true;
                break;
            }
        }
        if(!check)
        {
            var newUser = new UserEntity
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email,
                Phone = user.Phone,
                Code = user.Code,
                Status = user.Status,
                Balance = user.Balance
            };
            await _userRepo.CreateAsync(user);
        }
    }
    public async Task Update(Guid UserId, UserEntity user) {
        var target = await GetById(UserId);
        if(target is not null)
        {
            await _userRepo.UpdateAsync(user);
        }
    }
    public async Task Delete(Guid UserId) { }
}
