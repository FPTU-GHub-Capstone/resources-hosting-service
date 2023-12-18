using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/users")]
public class UsersController : BaseController
{
    private readonly IUserServices _userServices;
    private readonly ICharacterServices _characterServices;
    private readonly IGameUserServices _gameUserServices;
    private readonly IPaymentServices _paymentServices;
    public UsersController(IUserServices userServices, IGameUserServices gameUserServices
        , IPaymentServices paymentServices, ICharacterServices characterServices)
    {
        _userServices = userServices;
        _gameUserServices = gameUserServices;
        _paymentServices = paymentServices;
        _characterServices = characterServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? email)
    { 
        RequiredScope("games:*:get", "users:*:get");
        return Ok(await _userServices.List(email));
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
}