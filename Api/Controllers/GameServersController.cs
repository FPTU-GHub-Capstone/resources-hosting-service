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
    public GameServersController(IGameServerServices gameServerServices)
    {
        _gameServerServices = gameServerServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetGameServers()
    {
        RequiredScope("gameservers:*:get");
        return Ok(await _gameServerServices.List());
    }
}