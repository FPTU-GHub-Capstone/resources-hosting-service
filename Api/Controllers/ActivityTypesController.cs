using AutoWrapper.Filters;
using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/activity-types")]
public class ActivityTypesController : BaseController
{
    private readonly IActivityTypeServices _activityTypeServices;
    public ActivityTypesController(IActivityTypeServices activityTypeServices)
    {
        _activityTypeServices = activityTypeServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivityTypes()
    {
        RequiredScope("games:*:get", "activitytypes:*:get");
        return Ok(await _activityTypeServices.List());
    }
}
