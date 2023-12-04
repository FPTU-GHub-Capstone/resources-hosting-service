using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class UserServices : IUserServices
{
    private readonly IGenericRepository<UserEntity> _userRepo;
    public UserServices(IGenericRepository<UserEntity> userRepo)
    {
        _userRepo = userRepo;
    }
    public async Task<ICollection<UserEntity>> List(string? email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            return await _userRepo.WhereAsync(u => u.Email.Equals(email));
        }
        return await _userRepo.ListAsync();
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        return await _userRepo.FoundOrThrowAsync(UserId,
           Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
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
