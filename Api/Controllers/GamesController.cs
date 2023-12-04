using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Text.Json.Nodes;
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
    public GamesController(IGameServices gameServices, IActivityTypeServices activityTypeServices, IGameUserServices gameUserServices
        , IAssetTypeServices assetTypeServices, ICharacterTypeServices characterTypeServices 
        , IGameServerServices gameServerServices, ILevelServices levelServices
        , IWalletCategoryServices walletCategoryServices, IUserServices userServices
        , IGenericRepository<GameEntity> gameRepo, IGenericRepository<UserEntity> userRepo)
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
    }

    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] Guid[]? idList)
    {
        if (idList != null && idList.Count() > 0)
        {
            return Ok(await _gameServices.List(idList));
        }
        return Ok(await _gameServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
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
        return CreatedAtAction(nameof(GetGame), new { id = gameEntity.Id }, gameEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServices.Delete(id);
        return NoContent();
    }
}