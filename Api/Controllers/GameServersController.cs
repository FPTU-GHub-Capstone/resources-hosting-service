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
    public GameServersController(IGameServerServices gameServerServices, IGenericRepository<GameServerEntity> gameServerRepo)
    {
        _gameServerServices = gameServerServices;
        _gameServerRepo = gameServerRepo;
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
        var newGameServer = new GameServerEntity();
        Mapper.Map(gameServer, newGameServer);
        await _gameServerServices.Create(newGameServer);
        return CreatedAtAction("GetGameServer", new { id = newGameServer.Id }, newGameServer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGameServer(Guid id, [FromBody] UpdateGameServerRequest gameServer)
    {
        var updateGameServer = await _gameServerServices.GetById(id);
        Mapper.Map(gameServer, updateGameServer);
        await _gameServerServices.Update(id, updateGameServer);
        return Ok(updateGameServer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameServer(Guid id)
    {
        await _gameServerServices.Delete(id);
        return NoContent();
    }
}
