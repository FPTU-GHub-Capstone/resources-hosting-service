using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.Repositories;
using System.Collections.Generic;
using System;

namespace ServiceLayer.Business;

public class GameUserServices : IGameUserServices
{
    public readonly IGenericRepository<GameUserEntity> _gameUserRepo;
    public readonly IGenericRepository<GameEntity> _gameRepo;
    public readonly IGenericRepository<UserEntity> _userRepo;
    public GameUserServices(IGenericRepository<GameUserEntity> gameUserRepo, IGenericRepository<GameEntity> gameRepo, IGenericRepository<UserEntity> userRepo)
    {
        _gameUserRepo = gameUserRepo;
        _gameRepo = gameRepo;
        _userRepo = userRepo;
    }
    public async Task<ICollection<GameUserEntity>> List()
    {
        var list = await _gameUserRepo.ListAsync();
        return list;
    }
    public async Task<GameUserEntity> GetById(Guid id)
    {
        var gu = await _gameUserRepo.FoundOrThrowAsync(id,"This data is " + Constants.ERROR.NOT_EXIST_ERROR, new string[] { "User", "Game" });
        return gu;
    }
    public async Task<List<UserEntity>> GetUserByGameId(Guid id)
    {
        var game = await _gameUserRepo.WhereAsync(gu => gu.GameId == id,new string[] {"User"});
        return game.Select(g => g.User).ToList();
    }
    public async Task Create(GameUserEntity gameUser)
    {
        await CheckDuplicateGameAndUser(gameUser);
        await _gameUserRepo.CreateAsync(gameUser);
    }
    public async Task Delete(Guid id)
    {
        var gameUser = await _gameUserRepo.FoundOrThrowAsync(id);
        await _gameUserRepo.DeleteAsync(gameUser);
    }

    public async Task CheckDuplicateGameAndUser(GameUserEntity gu)
    {
        var checkGameUser = await _gameUserRepo.FirstOrDefaultAsync(
            guser => guser.GameId.Equals(gu.GameId) && guser.UserId.Equals(gu.UserId));
        if (checkGameUser is not null)
        {
            throw new BadRequestException("This user is " + Constants.ERROR.ALREADY_EXIST_ERROR + " in this game");
        }
    }
}