using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : BaseController
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
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.List();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userServices.GetById(Guid.Parse(id));
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity user)
        {
            await _userServices.Create(user);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserEntity user)
        {
            await _userServices.Update(Guid.Parse(id), user);
            return Ok();
        }
    }
}
