using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/level-progresses")]
public class LevelProgressesController : BaseController
{
    private readonly ILevelProgressServices _levelProgressServices;
    public LevelProgressesController(ILevelProgressServices levelProgressServices)
    {
        _levelProgressServices = levelProgressServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetLevelProgresses()
    {
        RequiredScope("levelprogresses:*:get");
        return Ok(await _levelProgressServices.List());
    }
}