using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApiLayer.Configurations.AppConfig;
using WebApiLayer.UserFeatures.Requests;
using WebApiLayer.UserFeatures.Response;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms")]
public class AuthController : BaseController
{
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;
    private readonly IUserServices _userServices;
    private readonly IGenericRepository<UserEntity> _userRepo;
    public AuthController(IOptions<AppSettings> appSettings, IUserServices userServices, IGenericRepository<UserEntity> userRepo)
    {
        _appSettings = appSettings.Value;
        _client = new HttpClient
        {
            BaseAddress = new Uri(appSettings.Value.IdpUrl),
        };
        _userServices = userServices;
        _userRepo = userRepo;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        string loginEndpoint = $"{_client.BaseAddress}/login";
        var jsonData = BuildJsonLoginReqBody(loginRequest);
        var contentData = new StringContent(jsonData, Encoding.UTF8, Constants.Http.JSON_CONTENT_TYPE);
        var response = await _client.PostAsync(loginEndpoint, contentData);
        if (! response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException();
        }
        var result = await BuildJsonResponse<IdpLoginResponse>(response);
        return CreatedAtAction(nameof(Login), result);
    }

    private string BuildJsonLoginReqBody(LoginRequest loginRequest)
    {
        var reqData = new Dictionary<string, string>
        {
            { "username", loginRequest.Username },
            { "password", loginRequest.Password }
        };
       return JsonConvert.SerializeObject(reqData);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        string registerEndpoint = $"{_client.BaseAddress}/register";
        var jsonData = BuildJsonRegisterReqBody(registerRequest);
        var contentData = new StringContent(jsonData, Encoding.UTF8, Constants.Http.JSON_CONTENT_TYPE);
        var response = await _client.PostAsync(registerEndpoint, contentData);
        if (!response.IsSuccessStatusCode)
        {
            throw new BadRequestException(await BuildJsonResponse<object>(response));
        }
        var result = await BuildJsonResponse<IdpRegisterResponse>(response);
        await _userServices.Create(new UserEntity
        {
            Username = result.username,
            Uid = result.uid,
        });
        return CreatedAtAction(nameof(Register), result);
    }

    private string BuildJsonRegisterReqBody(RegisterRequest registerRequest)
    {
        var reqData = new Dictionary<string, string>
        {
            { "username", registerRequest.Username },
            { "password", registerRequest.Password },
            { "reenterPassword", registerRequest.ReenterPassword },
        };
        return JsonConvert.SerializeObject(reqData);
    }
}