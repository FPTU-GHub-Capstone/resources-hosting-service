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
    private readonly ICharacterServices _characterServices;
    private readonly IGameUserServices _gameUserServices;
    private readonly IPaymentServices _paymentServices;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<GameUserEntity> _gameUserRepo;
    public UsersController(IUserServices userServices, IGenericRepository<UserEntity> userRepo
        , IGameUserServices gameUserServices, IPaymentServices paymentServices
        , IGenericRepository<GameUserEntity> gameUserRepo, ICharacterServices characterServices)
    {
        _userServices = userServices;
        _userRepo = userRepo;
        _gameUserServices = gameUserServices;
        _paymentServices = paymentServices;
        _gameUserRepo = gameUserRepo;
        _characterServices = characterServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? email)
    { 
        return Ok(await _userServices.List(email));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        return Ok(await _userServices.GetById(id));
    }

    [HttpGet("{id}/characters")]
    public async Task<IActionResult> GetCharByUserID(Guid id)
    {
        return Ok(await _characterServices.ListCharByUserId(id));
    }

    [HttpGet("{id}/games")]
    public async Task<IActionResult> GetGameByUserID(Guid id)
    {
        return Ok(await _gameUserServices.ListGamesByUserId(id));
    }

    [HttpGet("{id}/payments")]
    public async Task<IActionResult> GetPaymentByUserID(Guid id)
    {
        return Ok(await _paymentServices.ListPaymentByUserId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest cUser)
    {
        var user = new UserEntity();
        Mapper.Map(cUser, user);
        await _userServices.Create(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPost("{id}/add-game")]
    public async Task<IActionResult> CreateGameUser(Guid id, [FromQuery] Guid GameId)
    {
        var gameUser = new GameUserEntity {
            UserId = id,
            GameId = GameId
        };
        await _gameUserServices.Create(gameUser);
        return CreatedAtAction(nameof(GetUser), new {id = id}, gameUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest user)
    {
        var updateUser = await _userRepo.FoundOrThrowAsync(id, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(user, updateUser);
        await _userServices.Update(updateUser);
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userRepo.FoundOrThrowAsync(id, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        await _userServices.Delete(id);
        return NoContent();
    }

    [HttpDelete("{id}/delete-game")]
    public async Task<IActionResult> DeleteGameUser(Guid id)
    {
        await _gameUserRepo.FoundOrThrowAsync(id, Constants.ERROR.NOT_EXIST_ERROR);
        await _gameUserServices.Delete(id);
        return NoContent();
    }
}