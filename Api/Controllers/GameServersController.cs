using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/game-servers")]
public class GameServersController : BaseController
{
    private readonly IGameServerServices _gameServerServices;
    private readonly IGenericRepository<GameServerEntity> _gameServerRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public GameServersController(IGameServerServices gameServerServices, IGenericRepository<GameServerEntity> gameServerRepo, IGenericRepository<GameEntity> gameRepo)
    {
        _gameServerServices = gameServerServices;
        _gameServerRepo = gameServerRepo;
        _gameRepo = gameRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetGameServers()
    {
        return Ok(await _gameServerServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameServer(Guid id)
    {
        return Ok(await _gameServerServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGameServer([FromBody] CreateGameServerRequest gameServer)
    {
        await _gameRepo.FoundOrThrowAsync(gameServer.GameId, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        var newGameServer = new GameServerEntity();
        Mapper.Map(gameServer, newGameServer);
        await _gameServerServices.Create(newGameServer);
        return CreatedAtAction(nameof(GetGameServer), new { id = newGameServer.Id }, newGameServer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGameServer(Guid id, [FromBody] UpdateGameServerRequest gameServer)
    {
        var updateGameServer = await _gameServerRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME_SERVER + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(gameServer, updateGameServer);
        await _gameServerServices.Update(updateGameServer);
        return Ok(updateGameServer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameServer(Guid id)
    {
        await _gameServerRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME_SERVER + Constants.ERROR.NOT_EXIST_ERROR);
        await _gameServerServices.Delete(id);
        return NoContent();
    }
}
