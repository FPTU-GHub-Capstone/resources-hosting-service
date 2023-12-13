using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/game-servers")]
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
        if (!CurrentScp.Contains("gameservers:*:get"))
        {
            throw new ForbiddenException();
        }
        return Ok(await _gameServerServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameServer(Guid id)
    {
        return Ok(await _gameServerServices.GetById(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGameServer(Guid id, [FromBody] UpdateGameServerRequest gameServer)
    {
        var updateGameServer = await _gameServerRepo.FoundOrThrowAsync(id, Constants.Entities.GAME_SERVER + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(gameServer, updateGameServer);
        await _gameServerServices.Update(updateGameServer);
        return Ok(updateGameServer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameServer(Guid id)
    {
        await _gameServerRepo.FoundOrThrowAsync(id, Constants.Entities.GAME_SERVER + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServerServices.Delete(id);
        return NoContent();
    }
}