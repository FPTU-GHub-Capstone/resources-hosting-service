using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepositoryLayer.Repositories;
using ServiceLayer.Extensions;
using System.Data;

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
        foreach(var User in userList.Result)
        {
            if(User.Email == user.Email)
            {
                throw new BadRequestException("Email exists");
            }
        }
        await _userRepo.CreateAsync(user);
    }
    public async Task Update(Guid UserId, UserEntity user) {
        var target = await GetById(UserId);
        if(target is not null)
        {
            await _userRepo.UpdateAsync(target);
        }
        else
        {
            throw new NotFoundException("User not exist");
        }
    }
    public async Task Delete(Guid UserId) {
        var target = await _userRepo.DeleteSoftAsync(UserId);
        if(target is null)
        {
            throw new NotFoundException("User not exist");
        }
    }
}
