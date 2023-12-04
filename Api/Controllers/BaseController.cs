using AutoMapper;
using DomainLayer.Constants;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApiLayer.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected string CurrentUid => GetCurrentUid();

    protected string[] CurrentScp => GetCurrentScp();

    protected string GetCurrentUid()
    {
        var uid = HttpContext.User.Claims.FirstOrDefault(a => a.Type == "uid") ?? throw new ForbiddenException();
        return uid.Value;
    }

    protected string[] GetCurrentScp()
    {
        var scp = HttpContext.User.Claims.Where(a => a.Type == Constants.HttpContext.SCP)
                .Select(scope => scope.Value).ToArray();
        return scp;
    }
}
