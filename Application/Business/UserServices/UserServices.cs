using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class UserServices : IUserServices
{
    public readonly IGenericRepository<UserEntity> _userRepo;
    public readonly IGenericRepository<GameEntity> _gameRepo;
    public UserServices(IGenericRepository<UserEntity> userRepo, IGenericRepository<GameEntity> gameRepo)
    {
        _userRepo = userRepo;
        _gameRepo = gameRepo;
    }
    public async Task<ICollection<UserEntity>> List(string? email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            var user = await _userRepo.WhereAsync(u => u.Email.Equals(email));
            return user;
        }
        return await _userRepo.ListAsync();
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        return await _userRepo.FoundOrThrowAsync(UserId,
           Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<UserEntity>> GetByGameId(Guid gameId)
    {
        return await _userRepo.WhereAsync(
            user => user.Games.Any(game => game.Id == gameId)
        );
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
