using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using WebApiLayer.Configurations.AppConfig;
using WebApiLayer.UserFeatures.Requests;
using WebApiLayer.UserFeatures.Response;

namespace WebApiLayer.Controllers;

[Authorize]
[Route(Constants.Http.API_VERSION + "/gms/games")]
public class GamesController : BaseController
{
    private readonly IGameServices _gameServices;
    private readonly IGameUserServices _gameUserServices;
    private readonly IActivityTypeServices _activityTypeServices;
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGameServerServices _gameServerServices;
    private readonly ILevelServices _levelServices;
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IUserServices _userServices;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;

    public GamesController(
        IGameServices gameServices, 
        IActivityTypeServices activityTypeServices, 
        IGameUserServices gameUserServices, 
        IAssetTypeServices assetTypeServices, 
        ICharacterTypeServices characterTypeServices, 
        IGameServerServices gameServerServices, 
        ILevelServices levelServices, 
        IWalletCategoryServices walletCategoryServices, 
        IUserServices userServices, 
        IGenericRepository<GameEntity> gameRepo, 
        IGenericRepository<UserEntity> userRepo,
        IOptions<AppSettings> appSettings)
    {
        _gameServices = gameServices;
        _activityTypeServices = activityTypeServices;
        _gameUserServices = gameUserServices;
        _assetTypeServices = assetTypeServices;
        _characterTypeServices = characterTypeServices;
        _gameServerServices = gameServerServices;
        _levelServices = levelServices;
        _walletCategoryServices = walletCategoryServices;
        _userServices = userServices;
        _gameRepo = gameRepo;
        _userRepo = userRepo;
        _appSettings = appSettings.Value;
        _client = new HttpClient
        {
            BaseAddress = new Uri(appSettings.Value.IdpUrl),
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        //var stringScope = await GetUserScope();
        //if (stringScope.Contains("games:*:get"))
        //{
        //    return Ok(await _gameServices.List());
        //}
        //var scope = stringScope.Split(' ');
        //var getGamePattern = @"^games:(?<id>[^:]+):get$";
        //var ids = scope.Where(scp => Regex.IsMatch(scp, getGamePattern)).Select(scp => scp.Split(':')[1]); // games:{id}:get
        //var guids = ids.Select(Guid.Parse).ToArray();
        //return Ok(await _gameServices.List(guids));

        if (CurrentScp.Contains("games:*:get"))
        {
            return Ok(await _gameServices.List());
        }
        var getGamePattern = @"^games:(?<id>[^:]+):get$";
        var ids = CurrentScp.Where(scp => Regex.IsMatch(scp, getGamePattern)).Select(scp => scp.Split(':')[1]); // games:{id}:get
        var guids = ids.Select(Guid.Parse).ToArray();
        return Ok(await _gameServices.List(guids));
    }

    private async Task<string> GetUserScope()
    {
        var enpoint = $"{_client.BaseAddress}/profile";
        using (var request = new HttpRequestMessage(HttpMethod.Get, enpoint))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CurrentToken);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await BuildJsonResponse<GetProfileResponse>(response);
            return result.scope;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        //var stringScope = await GetUserScope();
        //if (!stringScope.Contains("games:*:get") && !stringScope.Contains($"games:{id}:get"))
        //{
        //    throw new ForbiddenException();
        //}
        //return Ok(await _gameServices.GetById(id));

        CheckGetGamePermission(id);
        return Ok(await _gameServices.GetById(id));
    }

    [HttpGet("{id}/activity-types")]
    public async Task<IActionResult> GetActTypeByGameID(Guid id)
    {
        return Ok(await _activityTypeServices.ListActTypesByGameId(id));
    }

    [HttpGet("{id}/asset-types")]
    public async Task<IActionResult> GetAssTypeByGameID(Guid id)
    {
        return Ok(await _assetTypeServices.ListAssTypesByGameId(id));
    }

    [HttpGet("{id}/character-types")]
    public async Task<IActionResult> GetCharTypeByGameID(Guid id)
    {
        var ctList = await _characterTypeServices.ListCharTypesByGameId(id);
        List<CharacterTypeResponse> ctListResponse = new();
        foreach (var ct in ctList)
        {
            var ctResponse = new CharacterTypeResponse();
            Mapper.Map(ct, ctResponse);
            ctResponse.BaseProperties = JsonObject.Parse(ct.BaseProperties);
            ctListResponse.Add(ctResponse);
        }
        return Ok(ctListResponse);
    }

    [HttpGet("{id}/game-servers")]
    public async Task<IActionResult> GetGameServerByGameID(Guid id)
    {
        return Ok(await _gameServerServices.ListServersByGameId(id));
    }

    [HttpGet("{id}/levels")]
    public async Task<IActionResult> GetLevelByGameID(Guid id)
    {
        return Ok(await _levelServices.ListLevelsByGameId(id));
    }

    [HttpGet("{id}/wallet-categories")]
    public async Task<IActionResult> GetWalCatByGameID(Guid id)
    {
        return Ok(await _walletCategoryServices.ListWalCatsByGameId(id));
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetUsersByGameID(Guid id)
    {
        return Ok(await _gameUserServices.ListUsersByGameId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest newGame)
    {
        var gameEntity = new GameEntity();
        Mapper.Map(newGame, gameEntity);
        await _gameServices.Create(gameEntity);

        var gameId = gameEntity.Id.ToString();
        await UpdateUserScope(gameId);
        return CreatedAtAction(nameof(GetGame), new { id = gameId }, gameEntity);
    }

    private async Task UpdateUserScope(string gameId)
    {
        var enpoint = $"{_client.BaseAddress}/users/{CurrentUid}/add-scope";
        var jsonData = BuildJsonUpdateScopeReqBody(gameId);
        using (var request = new HttpRequestMessage(HttpMethod.Put, enpoint))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CurrentToken);
            request.Content = new StringContent(jsonData, Encoding.UTF8, Constants.Http.JSON_CONTENT_TYPE);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
    private string BuildJsonUpdateScopeReqBody(string gameId)
    {
        var scope = new string[] {
            $"games:{gameId}:get",
            $"games:{gameId}:update",
            $"games:{gameId}:delete",
        } ;
        var reqData = new Dictionary<string, object>
        {
            { "scope", scope },
        };
        return JsonConvert.SerializeObject(reqData);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        CheckUpdateGamePermission(id);
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpPut("{id}/upgrade-plan")]
    public async Task<IActionResult> UpgradeGame(Guid id, [FromBody] UpgradePlanRequest req)
    {
        CheckUpdateGamePermission(id);
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        if (req.GamePlan <= updateGame.GamePlan)
        {
            throw new BadRequestException("Cannot downgrade plan");
        }
        updateGame.GamePlan = req.GamePlan;
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        CheckDeleteGamePermission(id);
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServices.Delete(id);
        return NoContent();
    }
}