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
        var userCheck = await _userRepo.FirstOrDefaultAsync(
            u=>u.Email.Equals(user.Email) || u.Username.Equals(user.Username));
        if(userCheck != null) {
            throw new BadRequestException("Email/Username already exists.");
        }
        await _userRepo.CreateAsync(user);
    }
    public async Task Update(UserEntity user) {
        await _userRepo.UpdateAsync(user);
    }
    public async Task Delete(Guid UserId) {
        await _userRepo.DeleteSoftAsync(UserId);
    }
}
