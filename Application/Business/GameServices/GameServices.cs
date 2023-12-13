using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Reflection.Metadata;

namespace ServiceLayer.Business;

public class GameServices : IGameServices
{
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IActivityTypeServices _activityTypeService;
    private readonly IAssetTypeServices _assetTypeService;
    private readonly IAssetServices _assetService;
    private readonly ICharacterServices _characterService;
    private readonly ICharacterTypeServices _characterTypeService;
    private readonly ILevelServices _levelService;
    private readonly IWalletCategoryServices _walletCategoryService;

    public GameServices(IGenericRepository<GameEntity> gameRepo, IActivityTypeServices activityTypeService
        , IAssetTypeServices assetTypeService, ICharacterServices characterService, ICharacterTypeServices characterTypeService
        , ILevelServices levelService, IWalletCategoryServices walletCategoryService)
    {
        _gameRepo = gameRepo;
        _activityTypeService = activityTypeService;
        _assetTypeService = assetTypeService;
        _characterService = characterService;
        _characterTypeService = characterTypeService;
        _levelService = levelService;
        _walletCategoryService = walletCategoryService;
    }
    public async Task<ICollection<GameEntity>> List()
    {
        return await _gameRepo.ListAsync();
    }
    public async Task<GameEntity> GetById(Guid gameId)
    {
        return await _gameRepo.FoundOrThrowAsync(gameId,
            Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<GameEntity>> List(Guid[] gameIds)
    {
        List<GameEntity> result = new List<GameEntity>();
        foreach(var gameId in gameIds.Distinct())
        {
            var game = await _gameRepo.FindByIdAsync(gameId);
            if (game is not null)
            {
                result.Add(game);
            }
        }
        return result;
    }
    public async Task<int> CountRecord(Guid id)
    {
        return (await _activityTypeService.ListActTypesByGameId(id)).Count()
            + (await _assetTypeService.ListAssTypesByGameId(id)).Count()
            + (await _assetService.ListAssetsByGameId(id)).Count()
            + (await _characterService.ListCharByGameId(id)).Count()
            + (await _characterTypeService.ListCharTypesByGameId(id)).Count()
            + (await _levelService.ListLevelsByGameId(id)).Count()
            + (await _walletCategoryService.ListWalCatsByGameId(id)).Count();
    }
    public async Task Create(GameEntity game)
    {
        var gameCheck = await _gameRepo.FirstOrDefaultAsync(
            g => g.Name.Equals(game.Name));
        if (gameCheck != null)
        {
            throw new BadRequestException("Name already exist");
        }
        await _gameRepo.CreateAsync(game);
    }
    public async Task Update(GameEntity game)
    {
        await _gameRepo.UpdateAsync(game);
    }
    public async Task Delete(Guid gameId)
    {
        await _gameRepo.DeleteSoftAsync(gameId);
    }
}