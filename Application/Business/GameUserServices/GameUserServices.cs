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
        return await getGameAndUser(list);
    }
    public async Task<GameUserEntity> GetById(Guid id)
    {
        var gu = await _gameUserRepo.FoundOrThrowAsync(id);
        return await getGameAndUser(gu);
    }

    public async Task<ICollection<GameUserEntity>> GetByGameId(Guid id)
    {
        var game = await _gameUserRepo.WhereAsync(gu => gu.GameId == id);
        return await getGameAndUser(game);
    }

    public async Task<ICollection<GameUserEntity>> GetByUserId(Guid id)
    {
        var user = await _gameUserRepo.WhereAsync(gu => gu.UserId == id);
        return await getGameAndUser(user);
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

    public async Task<ICollection<GameUserEntity>> getGameAndUser(IList<GameUserEntity> list)
    {
        foreach (var item in list.Select((value, i) => (value, i)))
        {
            list[item.i].Game = await _gameRepo.FindByIdAsync(list[item.i].GameId);
            list[item.i].User = await _userRepo.FindByIdAsync(list[item.i].UserId);
        }
        return list;
    }
    public async Task<GameUserEntity> getGameAndUser(GameUserEntity gu)
    {
        gu.Game = await _gameRepo.FindByIdAsync(gu.GameId);
        gu.User = await _userRepo.FindByIdAsync(gu.UserId);
        return gu;
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