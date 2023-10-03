using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("server/api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
}
