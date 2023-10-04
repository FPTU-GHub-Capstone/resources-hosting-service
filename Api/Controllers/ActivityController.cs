using Application.Interfaces;
using Application.Interfaces.Activity;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IActivityServices _activityServices;
        private readonly IGenericRepository<ActivityEntity> _activityRepo;
        public ActivityController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo)
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
}
