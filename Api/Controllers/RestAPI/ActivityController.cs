using Application.Interfaces;
using Application.Services.ActivityServices;
using Domain.Entities.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityServices _activityServices;
        private readonly IGenericRepository<ActivityEntity> _activityRepo;
        public ActivityController(IActivityServices activityServices, IGenericRepository<ActivityEntity> activityRepo)
        {
            _activityServices = activityServices;
            _activityRepo = activityRepo;
        }
        // GET: api/<ActivityController>
        [Route("Activity")]
        [HttpGet]
        public async Task<IActionResult> GetActivitíes()
        {
            var activities = await _activityServices.GetActivities();
            if(activities.Any())
            {
                return Ok(activities);
            }
            else
            {
                throw new Exception("0 item in list");
            }
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActivityController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActivityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
