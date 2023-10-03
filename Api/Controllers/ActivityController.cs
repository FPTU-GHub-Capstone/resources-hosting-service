using Application.Interfaces;
using Application.Interfaces.Activity;
using Domain.Entities.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
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
            var activities = await _activityServices.List();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
