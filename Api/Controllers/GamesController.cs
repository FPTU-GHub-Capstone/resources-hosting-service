using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/games")]
public class GamesController : BaseController
{
    private readonly IGameServices _gameServices;
    private readonly IActivityTypeServices _activityTypeServices;
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGameServerServices _gameServerServices;
    private readonly ILevelServices _levelServices;
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    public GamesController(IGameServices gameServices, IActivityTypeServices activityTypeServices
        , IAssetTypeServices assetTypeServices
        , ICharacterTypeServices characterTypeServices , IGameServerServices gameServerServices
        , ILevelServices levelServices, IWalletCategoryServices walletCategoryServices
        , IGenericRepository<GameEntity> gameRepo, IGenericRepository<UserEntity> userRepo)
    {
        _gameServices = gameServices;
        _activityTypeServices = activityTypeServices;
        _assetTypeServices = assetTypeServices;
        _characterTypeServices = characterTypeServices;
        _gameServerServices = gameServerServices;
        _levelServices = levelServices;
        _walletCategoryServices = walletCategoryServices;
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

    [HttpGet("{id}/activity-type")]
    public async Task<IActionResult> GetActTypeByGameID(Guid id)
    {
        return Ok(await _activityTypeServices.GetByGameId(id));
    }

    [HttpGet("{id}/asset-type")]
    public async Task<IActionResult> GetAssTypeByGameID(Guid id)
    {
        return Ok(await _assetTypeServices.GetByGameId(id));
    }

    [HttpGet("{id}/character-type")]
    public async Task<IActionResult> GetCharTypeByGameID(Guid id)
    {
        return Ok(await _characterTypeServices.GetByGameId(id));
    }

    [HttpGet("{id}/game-server")]
    public async Task<IActionResult> GetGameServerByGameID(Guid id)
    {
        return Ok(await _gameServerServices.GetByGameId(id));
    }

    [HttpGet("{id}/level")]
    public async Task<IActionResult> GetLevelByGameID(Guid id)
    {
        return Ok(await _levelServices.GetByGameId(id));
    }

    [HttpGet("{id}/wallet-category")]
    public async Task<IActionResult> GetWalCatByGameID(Guid id)
    {
        return Ok(await _walletCategoryServices.GetByGameId(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest newGame)
    {
        await _userRepo.FoundOrThrowAsync(newGame.UserId, Constants.ENTITY.USER + Constants.ERROR.NOT_EXIST_ERROR);
        var gameEntity = new GameEntity();
        Mapper.Map(newGame, gameEntity);
        await _gameServices.Create(gameEntity);
        return CreatedAtAction(nameof(GetGame), new { id = gameEntity.Id }, gameEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        await _gameRepo.FoundOrThrowAsync(id, Constants.ENTITY.GAME + Constants.ERROR.NOT_EXIST_ERROR);
        await _gameServices.Delete(id);
        return NoContent();
    }
}