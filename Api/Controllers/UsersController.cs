using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Runtime.ConstrainedExecution;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/users")]
public class UsersController : BaseController
{
    private readonly IUserServices _userServices;
    private readonly IGameServices _gameServices;
    private readonly IGenericRepository<UserEntity> _userRepo;
    public UsersController(IUserServices userServices, IGenericRepository<UserEntity> userRepo, IGameServices gameServices)
    {
        _userServices = userServices;
        _userRepo = userRepo;
        _gameServices = gameServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? email)
    {
        var users = await _userServices.List(email);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userServices.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest cUser)
    {
        var user = new UserEntity();
        Mapper.Map(cUser, user);
        await _userServices.Create(user);
        if (cUser.GameId != null)
        {
            var updateUser = await _userRepo.FoundOrThrowAsync(user.Id, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
            var game = await _gameServices.GetById((Guid)cUser.GameId);
            updateUser.Games = new List<GameEntity> { game };
            await _userServices.Update(updateUser);
            user.Games = user.Games?.Select(g => new GameEntity { Id = g.Id }).ToList();
        }
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest user)
    {
        var updateUser = await _userRepo.FoundOrThrowAsync(id, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        if (user.GameId != null)
        {
            var game = await _gameServices.GetById((Guid)user.GameId);
            updateUser.Games = new List<GameEntity> { game };
        }
        Mapper.Map(user, updateUser);
        await _userServices.Update(updateUser);
        updateUser.Games = updateUser.Games?.Select(g => new GameEntity { Id = g.Id }).ToList();
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userRepo.FoundOrThrowAsync(id, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        await _userServices.Delete(id);
        return NoContent();
    }
}