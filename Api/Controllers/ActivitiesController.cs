using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/activities")]
public class ActivitiesController : BaseController
{
    private readonly IActivityServices _activityServices;
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    public ActivitiesController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo)
    {
        _activityServices = activityServices;
        _activityRepo = activityRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetActivitíes()
    {
        return Ok(await _activityServices.List());
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetActivityById(string id)
    {
        return Ok(await _activityServices.Search(Guid.Parse(id)));
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetActivityByName(string name)
    {
        return Ok(await _activityServices.Search(name));
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
