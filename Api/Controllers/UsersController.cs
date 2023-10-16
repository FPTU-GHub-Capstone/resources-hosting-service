﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/users")]
public class UsersController : BaseController
{
    private readonly IUserServices _userServices;
    public UsersController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userServices.List();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userServices.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest cUser)
    {
        var user = new UserEntity();
        Mapper.Map(cUser, user);
        await _userServices.Create(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest user)
    {
        var updateUser = await _userServices.GetById(id);
        Mapper.Map(user, updateUser);
        await _userServices.Update(id, updateUser);
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userServices.Delete(id);
        return NoContent();
    }
}
