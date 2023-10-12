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
    public GamesController(IGameServices gameServices, IGenericRepository<GameEntity> gameRepo)
    {
        _gameServices = gameServices;
        _gameRepo = gameRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        return Ok(await _gameServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        var user = await _gameServices.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest newGame)
    {
        var gameEntity = new GameEntity();
        Mapper.Map(newGame, gameEntity);
        await _gameServices.Create(gameEntity);
        return CreatedAtAction("GetGame", new { id = gameEntity.Id }, gameEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        var updateGame = await _gameRepo.FindByIdAsync(id);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(id, updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        await _gameServices.Delete(id);
        return NoContent();
    }
}