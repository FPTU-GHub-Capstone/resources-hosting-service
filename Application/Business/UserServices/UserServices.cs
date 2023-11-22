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
        IList<UserEntity> userList;
        if (!string.IsNullOrEmpty(email))
        {
            userList = await _userRepo.WhereAsync(u => u.Email.Equals(email));
        }
        else
        {
            userList = await _userRepo.ListAsync();
        }
        foreach (var singleUser in userList)
        {
            var gameuser = await _gameUserRepo.WhereAsync(gu => gu.UserId == singleUser.Id);
            if (gameuser != null)
            {
                foreach (var game in gameuser)
                {
                    singleUser.Games.Add(await _gameRepo.FirstOrDefaultAsync(g => g.Id == game.GameId));
                }
            }
        }
        return userList;
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        UserEntity user = await _userRepo.FoundOrThrowAsync(UserId,
           Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        var gameuser = await _gameUserRepo.WhereAsync(gu => gu.UserId == user.Id);
        if (gameuser != null)
        {
            foreach (var game in gameuser)
            {
                user.Games.Add(await _gameRepo.FirstOrDefaultAsync(g => g.Id == game.GameId));
            }
        }
        return user;
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
