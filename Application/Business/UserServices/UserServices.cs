using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

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
            await _userRepo.CreateAsync(user);
        }
    }
    public async Task Update(Guid UserId, UserEntity user) {
        var target = await GetById(UserId);
        if(target is not null)
        {
            target.Username = user.Username;
            target.Password = user.Password;
            target.FirstName = user.FirstName;
            target.LastName = user.LastName;
            target.Avatar = user.Avatar;
            target.Email = user.Email;
            target.Phone = user.Phone;
            target.Code = user.Code;
            target.Status = user.Status;
            target.Balance = user.Balance;
            await _userRepo.UpdateAsync(target);
        }
        else
        {
            throw new NotFoundException("User not exist");
        }
    }
    public async Task Delete(Guid UserId) {
        var user = await _userRepo.DeleteSoftAsync(UserId);
        if(user is null)
        {
            throw new NotFoundException("User not exist");
        }
    }
}
