using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class UserServices : IUserServices
{
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<GameUserEntity> _gameUserRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public UserServices(IGenericRepository<UserEntity> userRepo, IGenericRepository<GameUserEntity> gameUserRepo
        , IGenericRepository<GameEntity> gameRepo)
    {
        _userRepo = userRepo;
        _gameUserRepo = gameUserRepo;
        _gameRepo = gameRepo;
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
