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
    private readonly IActivityServices _activityServices;
    private readonly IActivityTypeServices _activityTypeServices;
    private readonly IAssetAttributeServices _assetAttributeServices;
    private readonly IAssetServices _assetServices;
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly IAttributeGroupServices _attributeGroupServices;
    private readonly ICharacterAssetServices _characterAssetServices;
    private readonly ICharacterAttributeServices _characterAttributeServices;
    private readonly ICharacterServices _characterServices;
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGameServices _gameServices;
    private readonly IGameServerServices _gameServerServices;
    private readonly IGameUserServices _gameUserServices;
    private readonly ILevelProgressServices _levelProgressServices;
    private readonly ILevelServices _levelServices;
    private readonly IPaymentServices _paymentServices;
    private readonly ITransactionServices _transactionServices;
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IWalletServices _walletServices;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;

    public GamesController(IActivityServices activityServices
        , IActivityTypeServices activityTypeServices
        , IAssetAttributeServices assetAttributeServices
        , IAssetServices assetServices
        , IAssetTypeServices assetTypeServices
        , IAttributeGroupServices attributeGroupServices
        , ICharacterAssetServices characterAssetServices
        , ICharacterAttributeServices characterAttributeServices
        , ICharacterServices characterServices
        , ICharacterTypeServices characterTypeServices
        , IGameServices gameServices
        , IGameServerServices gameServerServices
        , IGameUserServices gameUserServices
        , ILevelProgressServices levelProgressServices
        , ILevelServices levelServices
        , IPaymentServices paymentServices
        , ITransactionServices transactionServices
        , IWalletCategoryServices walletCategoryServices
        , IWalletServices walletServices
        , IGenericRepository<GameEntity> gameRepo
        , IGenericRepository<UserEntity> userRepo
        , IOptions<AppSettings> appSettings)
    {
        _activityServices = activityServices;
        _activityTypeServices = activityTypeServices;
        _assetAttributeServices = assetAttributeServices;
        _assetTypeServices = assetTypeServices;
        _assetServices = assetServices;
        _attributeGroupServices = attributeGroupServices;
        _characterAssetServices = characterAssetServices;
        _characterAttributeServices = characterAttributeServices;
        _characterServices = characterServices;
        _characterTypeServices = characterTypeServices;
        _gameServices = gameServices;
        _gameUserServices = gameUserServices;
        _gameServerServices = gameServerServices;
        _levelProgressServices = levelProgressServices;
        _levelServices = levelServices;
        _paymentServices = paymentServices;
        _transactionServices = transactionServices;
        _walletCategoryServices = walletCategoryServices;
        _walletServices = walletServices;
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
        CheckGetGamePermission(id);
        return Ok(await _gameServices.GetById(id));
    }

    [HttpGet("{id}/activities")]
    public async Task<IActionResult> GetActivitiesByGameID(Guid id)
    {
        CheckGetActivityPermission(id);
        return Ok(await _activityServices.ListActivitiesByGameId(id));
    }

    [HttpGet("{id}/activity-types")]
    public async Task<IActionResult> GetActTypesByGameID(Guid id)
    {
        CheckGetActivityTypePermission(id);
        return Ok(await _activityTypeServices.ListActTypesByGameId(id));
    }

    [HttpGet("{id}/asset-attributes")]
    public async Task<IActionResult> GetAssetAttributesByGameID(Guid id)
    {
        CheckGetAssetAttributePermission(id);
        return Ok(await _assetAttributeServices.ListAssetAttributeByGameId(id));
    }

    [HttpGet("{id}/assets")]
    public async Task<IActionResult> GetAssetsByGameID(Guid id)
    {
        CheckGetAssetPermission(id);
        return Ok(await _assetServices.ListAssetsByGameId(id));
    }

    [HttpGet("{id}/asset-types")]
    public async Task<IActionResult> GetAssTypesByGameID(Guid id)
    {
        CheckGetAssetTypePermission(id);
        return Ok(await _assetTypeServices.ListAssTypesByGameId(id));
    }
    
    [HttpGet("{id}/attribute-groups")]
    public async Task<IActionResult> GetAttributeGroupsByGameID(Guid id)
    {
        CheckGetAttributeGroupPermission(id);
        var attGrpList = await _attributeGroupServices.ListAttributeGroupsByGameId(id);
        List<AttributeGroupResponse> attGrpListResponse = new();
        foreach (var ag in attGrpList)
        {
            var agResponse = new AttributeGroupResponse();
            Mapper.Map(ag, agResponse);
            agResponse.Effect = JsonObject.Parse(ag.Effect);
            attGrpListResponse.Add(agResponse);
        }
        return Ok(attGrpListResponse);
    }

    [HttpGet("{id}/character-assets")]
    public async Task<IActionResult> GetCharacterAssetsByGameID(Guid id)
    {
        CheckGetCharacterAssetPermission(id);
        return Ok(await _characterAssetServices.ListCharAssetsByGameId(id));
    }

    [HttpGet("{id}/character-attributes")]
    public async Task<IActionResult> GetCharacterAttributesByGameID(Guid id)
    {
        CheckGetCharacterAttributePermission(id);
        return Ok(await _characterAttributeServices.ListCharAttByGameId(id));
    }

    [HttpGet("{id}/characters")]
    public async Task<IActionResult> GetCharactersByGameID(Guid id)
    {
        CheckGetCharacterPermission(id);
        return Ok(await _characterServices.ListCharByGameId(id));
    }

    [HttpGet("{id}/character-types")]
    public async Task<IActionResult> GetCharTypesByGameID(Guid id)
    {
        CheckGetCharacterTypePermission(id);
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
    public async Task<IActionResult> GetGameServersByGameID(Guid id)
    {
        CheckGetGamePermission(id);
        return Ok(await _gameServerServices.ListServersByGameId(id));
    }

    [HttpGet("{id}/level-progreses")]
    public async Task<IActionResult> GetLevelProgressesByGameID(Guid id)
    {
        CheckGetLevelProgressPermission(id);
        return Ok(await _levelProgressServices.ListLevelProgByGameId(id));
    }

    [HttpGet("{id}/levels")]
    public async Task<IActionResult> GetLevelsByGameID(Guid id)
    {
        CheckGetGamePermission(id);
        return Ok(await _levelServices.ListLevelsByGameId(id));
    }

    [HttpGet("{id}/transactions")]
    public async Task<IActionResult> GetTransactionsByGameID(Guid id)
    {
        CheckGetTransactionPermission(id);
        return Ok(await _transactionServices.ListTransactionsByGameId(id));
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetUsersByGameID(Guid id)
    {
        CheckGetGamePermission(id);
        return Ok(await _gameUserServices.ListUsersByGameId(id));
    }

    [HttpGet("{id}/wallet-categories")]
    public async Task<IActionResult> GetWalCatsByGameID(Guid id)
    {
        CheckGetGamePermission(id);
        return Ok(await _walletCategoryServices.ListWalCatsByGameId(id));
    }

    [HttpGet("{id}/wallets")]
    public async Task<IActionResult> GetWalletsByGameID(Guid id)
    {
        CheckGetWalletPermission(id);
        return Ok(await _walletServices.ListWalletsByGameId(id));
    }

    [HttpGet("{id}/count-record")]
    public async Task<IActionResult> CountRecordsByGameId(Guid id)
    {
        CheckGetGamePermission(id);
        return Ok(await _gameServices.CountRecord(id));
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