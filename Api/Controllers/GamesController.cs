using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/games")]
public class GamesController : BaseController
{
    private readonly IGameServices _gameServices;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    public GamesController(IGameServices gameServices, IGenericRepository<GameEntity> gameRepo, IGenericRepository<UserEntity> userRepo)
    {
        _gameServices = gameServices;
        _gameRepo = gameRepo;
        _userRepo = userRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        return Ok(await _gameServices.List());
    }

    [HttpGet]
    [Route("idList")]
    public async Task<IActionResult> GetGames([FromQuery] Guid[] idList)
    {
        return Ok(await _gameServices.List(idList));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        return Ok(await _gameServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest newGame)
    {
        await _userRepo.FoundOrThrowAsync(newGame.UserId, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        var gameEntity = new GameEntity();
        Mapper.Map(newGame, gameEntity);
        await _gameServices.Create(gameEntity);
        return CreatedAtAction(nameof(GetGame), new { id = gameEntity.Id }, gameEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        await _gameRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        await _gameServices.Delete(id);
        return NoContent();
    }
}