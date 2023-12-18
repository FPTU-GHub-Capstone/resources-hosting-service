using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Runtime.InteropServices;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/character-attributes")]
public class CharacterAttributesController : BaseController
{
    private readonly ICharacterAttributeServices _charAttServices;
    public CharacterAttributesController(ICharacterAttributeServices charAttServices)
    {
        _charAttServices = charAttServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetCharacterAttributes()
    {
        RequiredScope("games:*:get", "characterattributes:*:get");
        return Ok(await _charAttServices.List());
    }
}