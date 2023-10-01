using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        // GET: api/<LevelControlelr>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LevelControlelr>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LevelControlelr>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LevelControlelr>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LevelControlelr>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
