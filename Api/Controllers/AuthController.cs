using DomainLayer.Constants;
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
        var stringData = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IdpLoginResponse>(stringData);
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
}