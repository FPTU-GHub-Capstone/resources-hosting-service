using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;
using DomainLayer.Exceptions;

namespace WebApiLayer.Controllers;

[Authorize]
[Route(Constants.Http.API_VERSION + "/gms/activities")]
public class ActivitiesController : BaseController
{
    private readonly IActivityServices _activityServices;
    public ActivitiesController(IActivityServices activityServices)
    {
        _activityServices = activityServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivitíes()
    {
        RequiredScope("activities:*:get");
        return Ok(await _activityServices.List());
    }
}