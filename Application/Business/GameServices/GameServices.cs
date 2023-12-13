using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Reflection.Metadata;

namespace ServiceLayer.Business;

public class GameServices : IGameServices
{
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IActivityServices _activityServices;
    private readonly IActivityTypeServices _activityTypeServices;
    private readonly IAssetServices _assetServices;
    private readonly IAssetTypeServices _assetTypeServices;
    private readonly ICharacterAssetServices _characterAssetServices;
    private readonly ICharacterServices _characterServices;
    private readonly ICharacterTypeServices _characterTypeServices;
    private readonly IGameServerServices _gameServerServices;
    private readonly ILevelProgressServices _levelProgressServices;
    private readonly ILevelServices _levelServices;
    private readonly ITransactionServices _transactionServices;
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IWalletServices _walletServices;

    public GameServices(IGenericRepository<GameEntity> gameRepo, 
        IActivityServices activityServices, IActivityTypeServices activityTypeServices
        , IAssetServices assetServices, IAssetTypeServices assetTypeServices
        , ICharacterServices characterServices, ICharacterTypeServices characterTypeServices
        , IGameServerServices gameServerServices, ILevelProgressServices levelProgressServices
        , ILevelServices levelServices, ITransactionServices transactionServices
        , IWalletCategoryServices walletCategoryServices, IWalletServices walletServices)
    {
        _gameRepo = gameRepo;
        _activityServices = activityServices;
        _activityTypeServices = activityTypeServices;
        _assetTypeServices = assetTypeServices;
        _assetServices = assetServices;
        _characterServices = characterServices;
        _characterTypeServices = characterTypeServices;
        _gameServerServices = gameServerServices;
        _levelProgressServices = levelProgressServices;
        _levelServices = levelServices;
        _transactionServices = transactionServices;
        _walletCategoryServices = walletCategoryServices;
        _walletServices = walletServices;
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
        return (await _activityServices.ListActivitiesByGameId(id)).Count()
            + (await _activityTypeServices.ListActTypesByGameId(id)).Count()
            + (await _assetTypeServices.ListAssTypesByGameId(id)).Count()
            + (await _assetServices.ListAssetsByGameId(id)).Count()
            + (await _characterServices.ListCharByGameId(id)).Count()
            + (await _characterTypeServices.ListCharTypesByGameId(id)).Count()
            + (await _gameServerServices.ListServersByGameId(id)).Count()
            + (await _levelServices.ListLevelsByGameId(id)).Count()
            + (await _levelProgressServices.ListLevelProgByGameId(id)).Count()
            + (await _transactionServices.ListTransactionsByGameId(id)).Count()
            + (await _walletCategoryServices.ListWalCatsByGameId(id)).Count()
            + (await _walletServices.ListWalletsByGameId(id)).Count();
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