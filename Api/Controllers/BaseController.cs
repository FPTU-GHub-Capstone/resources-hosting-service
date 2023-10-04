using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLayer.Controllers;

[Route("server/api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
}
