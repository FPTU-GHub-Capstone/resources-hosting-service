using DomainLayer.Constants;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebApiLayer.Configurations.AppConfig;
using WebApiLayer.UserFeatures.Requests;
using WebApiLayer.UserFeatures.Response;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms")]
public class AuthController : BaseController
{
    private readonly HttpClient _client;
    public AuthController(IOptions<AppSettings> appSettings)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(appSettings.Value.IdpUrl),
        };
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        string loginEndpoint = $"{_client.BaseAddress}/login";
        var jsonData = BuildJsonLoginReqBody(loginRequest);
        var contentData = new StringContent(jsonData, Encoding.UTF8, Constants.HTTP.JSON_CONTENT_TYPE);
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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        string registerEndpoint = $"{_client.BaseAddress}/register";
        var jsonData = BuildJsonRegisterReqBody(registerRequest);
        var contentData = new StringContent(jsonData, Encoding.UTF8, Constants.HTTP.JSON_CONTENT_TYPE);
        var response = await _client.PostAsync(registerEndpoint, contentData);
        if (!response.IsSuccessStatusCode)
        {
            throw new BadRequestException(await BuildJsonResponse<object>(response));
        }
        var result = await BuildJsonResponse<IdpRegisterResponse>(response);
        return CreatedAtAction(nameof(Login), result);
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

    private async Task<T> BuildJsonResponse<T>(HttpResponseMessage response)
    {
        var stringData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringData); ;
    }
}