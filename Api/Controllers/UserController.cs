using Application.Interfaces;
using Domain.Entities.Attribute;
using Domain.Entities.User;
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

        [Route("User")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.List();
            return Ok(users);
        }

        [Route("User/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userServices.GetById(Guid.Parse(id));
            return Ok(user);
        }
    }
}
