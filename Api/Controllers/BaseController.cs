using AutoMapper;
using DomainLayer.Constants;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WebApiLayer.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected string CurrentUid => GetCurrentUid();

    protected string CurrentSid => GetCurrentSid();

    protected string[] CurrentScp => GetCurrentScp();

    protected string CurrentToken => GetCurrentToken();

    protected string GetCurrentToken()
    {
        const string bearerPrefix = "Bearer ";
        var token = Request.Headers["Authorization"].ToString();
        return token.Substring(bearerPrefix.Length);
    }

    protected string GetCurrentUid()
    {
        var uid = HttpContext.User.Claims.FirstOrDefault(a => a.Type == Constants.HttpContext.UID) ?? throw new ForbiddenException();
        return uid.Value;
    }

    protected string GetCurrentSid()
    {
        var sid = HttpContext.User.Claims.FirstOrDefault(a => a.Type == Constants.HttpContext.SID) ?? throw new ForbiddenException();
        return sid.Value;
    }

    protected string[] GetCurrentScp()
    {
        var scp = HttpContext.User.Claims.Where(a => a.Type == Constants.HttpContext.SCP)
                .Select(scope => scope.Value).ToArray();
        return scp;
    }

    protected async Task<T> BuildJsonResponse<T>(HttpResponseMessage response)
    {
        var stringData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringData); ;
    }
}
