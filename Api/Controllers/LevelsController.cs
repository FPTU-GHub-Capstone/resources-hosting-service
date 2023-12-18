using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/levels")]
public class LevelsController : BaseController
{
    private readonly ILevelServices _levelServices;
    public LevelsController(ILevelServices levelServices)
    {
        _levelServices = levelServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetLevels()
    {
        RequiredScope("games:*:get", "levels:*:get");
        return Ok(await _levelServices.List());
    }
}