using Application.Interfaces;
using Domain.Entities.Attribute;
using Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.RestAPI
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IGenericRepository<Client> _clientRepo;
        private readonly IGenericRepository<UserEntity> _userRepo;
        public UserController(IUserServices userServices, IGenericRepository<Client> clientRepo, IGenericRepository<UserEntity> userRepo)
        {
            _userServices = userServices;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
        }
        // GET: api/<UserController>
        [Route("Client")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/User
        [Route("User")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetUsers();
            return Ok(users);
        }

        // GET api/User/5
        [Route("User/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var user = await _userServices.GetUser(Guid.Parse(id));
                return Ok(user);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
