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
    private readonly IUserServices _userServices;
    private readonly IWalletCategoryServices _walletCategoryServices;
    private readonly IWalletServices _walletServices;
    private readonly IGenericRepository<ActivityEntity> _activityRepo;
    private readonly IGenericRepository<ActivityTypeEntity> _activityTypeRepo;
    private readonly IGenericRepository<AssetAttributeEntity> _assetAttributeRepo;
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    private readonly IGenericRepository<AssetTypeEntity> _assetTypeRepo;
    private readonly IGenericRepository<AttributeGroupEntity> _attributeGroupRepo;
    private readonly IGenericRepository<CharacterAssetEntity> _characterAssetRepo;
    private readonly IGenericRepository<CharacterAttributeEntity> _characterAttributeRepo;
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    private readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    private readonly IGenericRepository<GameServerEntity> _gameServerRepo;
    private readonly IGenericRepository<GameUserEntity> _gameUserRepo;
    private readonly IGenericRepository<LevelEntity> _levelRepo;
    private readonly IGenericRepository<LevelProgressEntity> _levelProgressRepo;
    private readonly IGenericRepository<PaymentEntity> _paymentRepo;
    private readonly IGenericRepository<TransactionEntity> _transactionRepo;
    private readonly IGenericRepository<UserEntity> _userRepo;
    private readonly IGenericRepository<WalletCategoryEntity> _walletCategoryRepo;
    private readonly IGenericRepository<WalletEntity> _walletRepo;
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;

    public GamesController(IActivityServices activityServices, IActivityTypeServices activityTypeServices
        , IAssetAttributeServices assetAttributeServices, IAssetServices assetServices
        , IAssetTypeServices assetTypeServices, IAttributeGroupServices attributeGroupServices
        , ICharacterAssetServices characterAssetServices, ICharacterAttributeServices characterAttributeServices
        , ICharacterServices characterServices, ICharacterTypeServices characterTypeServices
        , IGameServices gameServices, IGameServerServices gameServerServices
        , IGameUserServices gameUserServices, ILevelProgressServices levelProgressServices
        , ILevelServices levelServices, IPaymentServices paymentServices
        , ITransactionServices transactionServices, IUserServices userServices
        , IWalletCategoryServices walletCategoryServices, IWalletServices walletServices
        , IGenericRepository<ActivityEntity> activityRepo, IGenericRepository<ActivityTypeEntity> activityTypeRepo
        , IGenericRepository<AssetAttributeEntity> assetAttributeRepo, IGenericRepository<AssetEntity> assetRepo
        , IGenericRepository<AssetTypeEntity> assetTypeRepo, IGenericRepository<AttributeGroupEntity> attributeGroupRepo
        , IGenericRepository<CharacterAssetEntity> characterAssetRepo, IGenericRepository<CharacterAttributeEntity> characterAttributeRepo
        , IGenericRepository<CharacterEntity> characterRepo, IGenericRepository<CharacterTypeEntity> characterTypeRepo
        , IGenericRepository<GameEntity> gameRepo, IGenericRepository<GameServerEntity> gameServerRepo
        , IGenericRepository<GameUserEntity> gameUserRepo, IGenericRepository<LevelEntity> levelRepo
        , IGenericRepository<LevelProgressEntity> levelProgressRepo, IGenericRepository<PaymentEntity> paymentRepo
        , IGenericRepository<TransactionEntity> transactionRepo, IGenericRepository<UserEntity> userRepo
        , IGenericRepository<WalletCategoryEntity> walletCategoryRepo, IGenericRepository<WalletEntity> walletRepo
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
        _userServices = userServices;
        _walletCategoryServices = walletCategoryServices;
        _walletServices = walletServices;
        _activityRepo = activityRepo;
        _activityTypeRepo = activityTypeRepo;
        _assetAttributeRepo = assetAttributeRepo;
        _assetRepo = assetRepo;
        _assetTypeRepo = assetTypeRepo;
        _attributeGroupRepo = attributeGroupRepo;
        _characterAssetRepo = characterAssetRepo;
        _characterAttributeRepo = characterAttributeRepo;
        _characterRepo = characterRepo;
        _characterTypeRepo = characterTypeRepo;
        _gameRepo = gameRepo;
        _gameUserRepo = gameUserRepo;
        _gameServerRepo = gameServerRepo;
        _levelRepo = levelRepo;
        _levelProgressRepo = levelProgressRepo;
        _paymentRepo = paymentRepo;
        _transactionRepo = transactionRepo;
        _userRepo = userRepo;
        _walletCategoryRepo = walletCategoryRepo;
        _walletRepo = walletRepo;
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
        RequiredScope("games:*:get", $"games:{id}:get");
        return Ok(await _gameServices.GetById(id));
    }

    #region Activities
    [HttpGet("{id}/activities")]
    public async Task<IActionResult> GetActivitiesByGameID([FromRoute] Guid id)
    {
        RequiredScope(
            $"games:{id}:get", 
            "activities:*:get", 
            $"activities:{id}:get"
        );
        return Ok(await _activityServices.ListActivitiesByGameId(id));
    }

    [HttpPost("{id}/activities")]
    public async Task<IActionResult> CreateActivity([FromRoute] Guid id, [FromBody] CreateActivityRequest act)
    {
        RequiredScope(
            "activities:create",
            $"activities:{id}:create",
            $"games:{id}:update"
        );
        await _activityTypeRepo.FoundOrThrowAsync(act.ActivityTypeId, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _transactionRepo.FoundOrThrowAsync(act.TransactionId, Constants.Entities.TRANSACTION + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(act.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newAct = new ActivityEntity();
        Mapper.Map(act, newAct);
        await _activityServices.Create(newAct);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetActivity), new { id = id, activityid = newAct.Id }, newAct);
    }

    [HttpGet("{id}/activities/{activityid}")]
    public async Task<IActionResult> GetActivity([FromRoute]Guid id, [FromRoute]Guid activityid)
    {
        RequiredScope(
            $"games:{id}:get",
            "activities:*:get",
            $"activities:{id}:get"
        );
        return Ok(await _activityServices.Search(activityid));
    }

    [HttpPut("{id}/activities/{activityid}")]
    public async Task<IActionResult> UpdateActivity([FromRoute] Guid id, [FromRoute] Guid activityid, [FromBody] UpdateActivityRequest act)
    {
        RequiredScope(
           $"activities:*:update",
           $"activities:{id}:update",
           $"games:{id}:update"
        );
        var updateAct = await _activityRepo.FoundOrThrowAsync(activityid, Constants.Entities.ACTIVITY + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(act, updateAct);
        await _activityServices.Update(updateAct);
        return Ok(updateAct);
    }

    [HttpDelete("{id}/activities/{activityid}")]
    public async Task<IActionResult> DeleteActivity([FromRoute] Guid id, [FromRoute] Guid activityid)
    {
        RequiredScope(
           $"activities:*:delete",
           $"activities:{id}:delete"
        );
        await _activityRepo.FoundOrThrowAsync(activityid, Constants.Entities.ACTIVITY + Constants.Errors.NOT_EXIST_ERROR);
        await _activityServices.Delete(activityid);
        return NoContent();
    }
    #endregion
    #region Activity Types
    [HttpGet("{id}/activity-types")]
    public async Task<IActionResult> GetActTypesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "activitytypes:*:get",
            $"activitytypes:{id}:get"
        );
        return Ok(await _activityTypeServices.ListActTypesByGameId(id));
    }

    [HttpPost("{id}/activity-types")]
    public async Task<IActionResult> CreateActivityType([FromRoute] Guid id, [FromBody] CreateActivityTypeRequest activityType)
    {
        RequiredScope(
            "activitytypes:create",
           $"activitytypes:{id}:create",
           $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(activityType.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newActivityType = new ActivityTypeEntity { GameId = id };
        Mapper.Map(activityType, newActivityType);
        await _activityTypeServices.Create(newActivityType);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetActivityType), new { id = id, activitytypeid = newActivityType.Id }, newActivityType);
    }

    [HttpGet("{id}/activity-types/{activitytypeid}")]
    public async Task<IActionResult> GetActivityType([FromRoute] Guid id, [FromRoute] Guid activitytypeid)
    {
        RequiredScope(
            $"games:{id}:get",
            "activitytypes:*:get",
            $"activitytypes:{id}:get"
        );
        return Ok(await _activityTypeServices.GetById(activitytypeid));
    }

    [HttpPut("{id}/activity-types/{activitytypeid}")]
    public async Task<IActionResult> UpdateActivityType([FromRoute] Guid id, [FromRoute] Guid activitytypeid, [FromBody] UpdateActivityTypeRequest activityType)
    {
        RequiredScope(
            "activitytypes:*:update",
            $"activitytypes:{id}:update",
            $"games:{id}:update"
        );
        var updateActivityType = await _activityTypeRepo.FoundOrThrowAsync(activitytypeid, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(activityType, updateActivityType);
        await _activityTypeServices.Update(updateActivityType);
        return Ok(updateActivityType);
    }

    [HttpDelete("{id}/activity-types/{activitytypeid}")]
    public async Task<IActionResult> DeleteActivityType([FromRoute] Guid id, [FromRoute] Guid activitytypeid)
    {
        RequiredScope(
            "activitytypes:*:delete",
            $"activitytypes:{id}:delete"
        );
        await _activityTypeRepo.FoundOrThrowAsync(activitytypeid, Constants.Entities.ACTIVITY_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _activityTypeServices.Delete(activitytypeid);
        return NoContent();
    }
    #endregion
    #region Asset Attributes
    [HttpGet("{id}/asset-attributes")]
    public async Task<IActionResult> GetAssetAttributesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "assetattributes:*:get",
            $"assetattributes:{id}:get"
        );
        return Ok(await _assetAttributeServices.ListAssetAttributeByGameId(id));
    }

    [HttpPost("{id}/asset-attributes")]
    public async Task<IActionResult> CreateAssetAttribute([FromRoute] Guid id, [FromBody] CreateAssetAttributeRequest assetAtt)
    {
        RequiredScope(
            "assetattributes:create",
            $"assetattributes:{id}:create",
            $"games:{id}:update"
        );
        await _assetRepo.FoundOrThrowAsync(assetAtt.AssetId, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _attributeGroupRepo.FoundOrThrowAsync(assetAtt.AttributeGroupId, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        var newAssAtt = new AssetAttributeEntity();
        Mapper.Map(assetAtt, newAssAtt);
        await _assetAttributeServices.Create(newAssAtt);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetAssetAttribute), new { id = id, assetattributeid = newAssAtt.Id }, newAssAtt);
    }
    
    [HttpGet("{id}/asset-attributes/{assetattributeid}")]
    public async Task<IActionResult> GetAssetAttribute([FromRoute] Guid id, [FromRoute] Guid assetattributeid)
    {
        RequiredScope(
            $"games:{id}:get",
            "assetattributes:*:get",
            $"assetattributes:{id}:get"
        );
        return Ok(await _assetAttributeServices.GetById(assetattributeid));
    }

    [HttpPut("{id}/asset-attributes/{assetattributeid}")]
    public async Task<IActionResult> UpdateAssetAttribute([FromRoute] Guid id, [FromRoute] Guid assetattributeid, [FromBody] UpdateAssetAttributeRequest assetAtt)
    {
        RequiredScope(
            "assetattributes:*:update",
            $"assetattributes:{id}:update",
            $"games:{id}:update"
        );
        var updateAssAtt = await _assetAttributeRepo.FoundOrThrowAsync(assetattributeid, Constants.Entities.ASSET_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(assetAtt, updateAssAtt);
        await _assetAttributeServices.Update(updateAssAtt);
        return Ok(updateAssAtt);
    }

    [HttpDelete("{id}/asset-attributes/{assetattributeid}")]
    public async Task<IActionResult> DeleteAssetAttribute([FromRoute] Guid id, [FromRoute] Guid assetattributeid)
    {
        RequiredScope(
            "assetattributes:*:delete",
            $"assetattributes:{id}:delete"
        );
        await _assetAttributeRepo.FoundOrThrowAsync(assetattributeid, Constants.Entities.ASSET_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        await _assetAttributeServices.Delete(assetattributeid);
        return NoContent();
    }
    #endregion
    #region Asset
    [HttpGet("{id}/assets")]
    public async Task<IActionResult> GetAssetsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "assets:*:get",
            $"assets:{id}:get"
        );
        return Ok(await _assetServices.ListAssetsByGameId(id));
    }

    [HttpPost("{id}/assets")]
    public async Task<IActionResult> CreateAsset([FromRoute] Guid id, [FromBody] CreateAssetRequest asset)
    {
        RequiredScope(
            "assets:create",
            $"assets:{id}:create",
            $"games:{id}:update"
        );
        await _assetTypeRepo.FoundOrThrowAsync(asset.AssetTypeId, Constants.Entities.ASSET_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        var newAsset = new AssetEntity();
        Mapper.Map(asset, newAsset);
        await _assetServices.Create(newAsset);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetAsset), new { id = id, assetid = newAsset.Id }, newAsset);
    }

    [HttpGet("{id}/assets/{assetid}")]
    public async Task<IActionResult> GetAsset([FromRoute] Guid id, [FromRoute] Guid assetid)
    {
        RequiredScope(
            $"games:{id}:get",
            "assets:*:get",
            $"assets:{id}:get"
        );
        return Ok(await _assetServices.GetById(assetid));
    }

    [HttpPut("{id}/assets/{assetid}")]
    public async Task<IActionResult> UpdateAsset([FromRoute] Guid id, [FromRoute] Guid assetid, [FromBody] UpdateAssetRequest asset)
    {
        RequiredScope(
            "assets:*:update",
            $"assets:{id}:update",
            $"games:{id}:update"
        );
        var updateAsset = await _assetRepo.FoundOrThrowAsync(assetid, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(asset, updateAsset);
        await _assetServices.Update(updateAsset);
        return Ok(updateAsset);
    }

    [HttpDelete("{id}/assets/{assetid}")]
    public async Task<IActionResult> DeleteAsset([FromRoute] Guid id, [FromRoute] Guid assetid)
    {
        RequiredScope(
            "assets:*:delete",
            $"assets:{id}:delete"
        );
        await _assetRepo.FoundOrThrowAsync(assetid, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _assetServices.Delete(assetid);
        return NoContent();
    }
    #endregion
    #region Asset Types

    [HttpGet("{id}/asset-types")]
    public async Task<IActionResult> GetAssTypesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "assettypes:*:get",
            $"assettypes:{id}:get"
        );
        return Ok(await _assetTypeServices.ListAssTypesByGameId(id));
    }

    [HttpPost("{id}/asset-types")]
    public async Task<IActionResult> CreateAssetType([FromRoute] Guid id, [FromBody] CreateAssetTypeRequest assetType)
    {
        RequiredScope(
            "assettypes:create",
            $"assettypes:{id}:create",
            $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var cAssetType = new AssetTypeEntity { GameId = id };
        Mapper.Map(assetType, cAssetType);
        await _assetTypeServices.Create(cAssetType);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetAssetType), new { id = id, assettypeid = cAssetType.Id }, cAssetType);
    }

    [HttpGet("{id}/asset-types/{assettypeid}")]
    public async Task<IActionResult> GetAssetType([FromRoute] Guid id, [FromRoute] Guid assettypeid)
    {
        RequiredScope(
            $"games:{id}:get",
            "assettypes:*:get",
            $"assettypes:{id}:get"
        );
        var assetType = await _assetTypeServices.GetById(assettypeid);
        return Ok(assetType);
    }

    [HttpPut("{id}/asset-types/{assettypeid}")]
    public async Task<IActionResult> UpdateAssetType([FromRoute] Guid id, [FromRoute] Guid assettypeid, [FromBody] UpdateAssetTypeRequest assetType)
    {
        RequiredScope(
            "assettypes:*:update",
            $"assettypes:{id}:update",
            $"games:{id}:update"
        );
        var uAssetType = await _assetTypeRepo.FoundOrThrowAsync(assettypeid, Constants.Entities.ASSET_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(assetType, uAssetType);
        await _assetTypeServices.Update(uAssetType);
        return Ok(uAssetType);
    }

    [HttpDelete("{id}/asset-types/{assettypeid}")]
    public async Task<IActionResult> DeleteAssetType([FromRoute] Guid id, [FromRoute] Guid assettypeid)
    {
        RequiredScope(
            "assettypes:*:delete",
            $"assettypes:{id}:delete"
        );
        await _assetTypeRepo.FoundOrThrowAsync(assettypeid, Constants.Entities.ASSET_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _assetTypeServices.Delete(assettypeid);
        return NoContent();
    }
    #endregion
    #region Attribute Groups

    [HttpGet("{id}/attribute-groups")]
    public async Task<IActionResult> GetAttributeGroupsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "attributegroups:*:get",
            $"attributegroups:{id}:get"
        );
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

    [HttpPost("{id}/attribute-groups")]
    public async Task<IActionResult> CreateAttributeGroup([FromRoute] Guid id, [FromBody] CreateAttributeGroupRequest attributeGroup)
    {
        RequiredScope(
            "attributegroups:create",
            $"attributegroups:{id}:create",
            $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var attGrpEnt = new AttributeGroupEntity { GameId = id };
        Mapper.Map(attributeGroup, attGrpEnt);
        attGrpEnt.Effect = attributeGroup.Effect.ToString();
        await _attributeGroupServices.Create(attGrpEnt);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetAttributeGroup), new { id = id, attributegroupid = attGrpEnt.Id }, attGrpEnt);
    }

    [HttpGet("{id}/attribute-groups/{attributegroupid}")]
    public async Task<IActionResult> GetAttributeGroup([FromRoute] Guid id, [FromRoute] Guid attributegroupid)
    {
        RequiredScope(
           $"games:{id}:get",
           "attributegroups:*:get",
           $"attributegroups:{id}:get"
        );
        var attribute = await _attributeGroupServices.GetById(attributegroupid);
        var agResponse = new AttributeGroupResponse();
        Mapper.Map(attribute, agResponse);
        agResponse.Effect = JsonObject.Parse(attribute.Effect);
        return Ok(agResponse);
    }

    [HttpPut("{id}/attribute-groups/{attributegroupid}")]
    public async Task<IActionResult> UpdateAttributeGroup([FromRoute] Guid id, [FromRoute] Guid attributegroupid, [FromBody] UpdateAttributeGroupRequest attributeGroup)
    {
        RequiredScope(
            "attributegroups:*:update",
            $"attributegroups:{id}:update",
            $"games:{id}:update"
        );
        var attGrpEnt = await _attributeGroupRepo.FoundOrThrowAsync(attributegroupid, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(attributeGroup, attGrpEnt);
        await _attributeGroupServices.Update(attGrpEnt);
        return Ok(attGrpEnt);
    }

    [HttpDelete("{id}/attribute-groups/{attributegroupid}")]
    public async Task<IActionResult> DeleteAttributeGroup([FromRoute] Guid id, [FromRoute] Guid attributegroupid)
    {
        RequiredScope(
            "attributegroups:*:delete",
            $"attributegroups:{id}:delete"
        );
        await _attributeGroupRepo.FoundOrThrowAsync(attributegroupid, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        await _attributeGroupServices.Delete(attributegroupid);
        return NoContent();
    }
    #endregion
    #region Character Assets
    [HttpGet("{id}/character-assets")]
    public async Task<IActionResult> GetCharacterAssetsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "characterassets:*:get",
            $"characterassets:{id}:get"
        );
        return Ok(await _characterAssetServices.ListCharAssetsByGameId(id));
    }

    [HttpPost("{id}/character-assets")]
    public async Task<IActionResult> CreateCharacterAsset([FromRoute] Guid id, [FromBody] CreateCharacterAssetRequest charAss)
    {
        RequiredScope(
            "characterassets:create",
            $"characterassets:{id}:create",
            $"games:{id}:update"
        );
        await _assetRepo.FoundOrThrowAsync(charAss.AssetsId, Constants.Entities.ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(charAss.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newCharAss = new CharacterAssetEntity();
        Mapper.Map(charAss, newCharAss);
        await _characterAssetServices.Create(newCharAss);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetCharacterAsset), new { id = id, characterassetid = newCharAss.Id }, newCharAss);
    }

    [HttpGet("{id}/character-assets/{characterassetid}")]
    public async Task<IActionResult> GetCharacterAsset([FromRoute] Guid id, [FromRoute] Guid characterassetid)
    {
        RequiredScope(
            $"games:{id}:get",
            "characterassets:*:get",
            $"characterassets:{id}:get"
        );
        var character = await _characterAssetServices.GetById(characterassetid);
        return Ok(character);
    }

    [HttpPut("{id}/character-assets/{characterassetid}")]
    public async Task<IActionResult> UpdateCharacterAsset([FromRoute] Guid id, [FromRoute] Guid characterassetid, [FromBody] UpdateCharacterAssetRequest charAss)
    {
        RequiredScope(
            "characterassets:*:update",
            $"characterassets:{id}:update",
            $"games:{id}:update"
        );
        var updateCharAss = await _characterAssetRepo.FoundOrThrowAsync(characterassetid, Constants.Entities.CHARACTER_ASSET + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charAss, updateCharAss);
        await _characterAssetServices.Update(updateCharAss);
        return Ok(updateCharAss);
    }

    [HttpDelete("{id}/character-assets/{characterassetid}")]
    public async Task<IActionResult> DeleteCharacterAsset([FromRoute] Guid id, [FromRoute] Guid characterassetid)
    {
        RequiredScope(
            "characterassets:*:delete",
            $"characterassets:{id}:delete"
        );
        await _characterAssetRepo.FoundOrThrowAsync(characterassetid, Constants.Entities.CHARACTER_ASSET + Constants.Errors.NOT_EXIST_ERROR);
        await _characterAssetServices.Delete(characterassetid);
        return NoContent();
    }
    #endregion
    #region Character Attributes
    [HttpGet("{id}/character-attributes")]
    public async Task<IActionResult> GetCharacterAttributesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "characterattributes:*:get",
            $"characterattributes:{id}:get"
        );
        return Ok(await _characterAttributeServices.ListCharAttByGameId(id));
    }

    [HttpPost("{id}/character-attributes")]
    public async Task<IActionResult> CreateCharacterAttribute([FromRoute] Guid id, [FromBody] CreateCharacterAttributeRequest charAtt)
    {
        RequiredScope(
            "characterattributes:create",
            $"characterattributes:{id}:create",
            $"games:{id}:update"
        );
        await _characterRepo.FoundOrThrowAsync(charAtt.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _attributeGroupRepo.FoundOrThrowAsync(charAtt.AttributeGroupId, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        var newCharAtt = new CharacterAttributeEntity();
        Mapper.Map(charAtt, newCharAtt);
        await _characterAttributeServices.Create(newCharAtt);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetCharacterAttribute), new { id = id, characterattributeid = newCharAtt.Id }, newCharAtt);
    }

    [HttpGet("{id}/character-attributes/{characterattributeid}")]
    public async Task<IActionResult> GetCharacterAttribute([FromRoute] Guid id, [FromRoute] Guid characterattributeid)
    {
        RequiredScope(
            $"games:{id}:get",
            "characterattributes:*:get",
            $"characterattributes:{id}:get"
        );
        var charAtt = await _characterAttributeServices.GetById(characterattributeid);
        return Ok(charAtt);
    }

    [HttpPut("{id}/character-attributes/{characterattributeid}")]
    public async Task<IActionResult> UpdateCharacterAttribute([FromRoute] Guid id, [FromRoute] Guid characterattributeid, [FromBody] UpdateCharacterAttributeRequest charAtt)
    {
        RequiredScope(
            "characterattributes:*:update",
            $"characterattributes:{id}:update",
            $"games:{id}:update"
        );
        var newCharAtt = await _characterAttributeRepo.FoundOrThrowAsync(characterattributeid, Constants.Entities.CHARACTER_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charAtt, newCharAtt);
        await _characterAttributeServices.Update(newCharAtt);
        return Ok(newCharAtt);
    }

    [HttpDelete("{id}/character-attributes/{characterattributeid}")]
    public async Task<IActionResult> DeleteCharacterAttribute([FromRoute] Guid id, [FromRoute] Guid characterattributeid)
    {
        RequiredScope(
            "characterattributes:*:delete",
            $"characterattributes:{id}:delete"
        );
        await _characterAttributeRepo.FoundOrThrowAsync(characterattributeid, Constants.Entities.CHARACTER_ATTRIBUTE + Constants.Errors.NOT_EXIST_ERROR);
        await _characterAttributeServices.Delete(characterattributeid);
        return NoContent();
    }
    #endregion
    #region Characters
    [HttpGet("{id}/characters")]
    public async Task<IActionResult> GetCharactersByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "characters:*:get",
            $"characters:{id}:get"
        );
        return Ok(await _characterServices.ListCharByGameId(id));
    }

    [AllowAnonymous]
    [HttpPost("{id}/characters")]
    public async Task<IActionResult> CreateCharacter([FromRoute] Guid id, [FromBody] CreateCharacterRequest character)
    {
        RequiredScope(
            "characters:create",
            $"characters:{id}:create",
            $"games:{id}:update"
        );
        await _userRepo.FoundOrThrowAsync(character.UserId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        await _characterTypeRepo.FoundOrThrowAsync(character.CharacterTypeId, Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServerRepo.FoundOrThrowAsync(character.GameServerId, Constants.Entities.GAME_SERVER + Constants.Errors.NOT_EXIST_ERROR);
        var newC = new CharacterEntity();
        Mapper.Map(character, newC);
        await _characterServices.Create(newC);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetCharacter), new { id = id, characterid = newC.Id }, newC);
    }

    [HttpGet("{id}/characters/{characterid}")]
    public async Task<IActionResult> GetCharacter([FromRoute] Guid id, [FromRoute] Guid characterid)
    {
        RequiredScope(
            $"games:{id}:get",
            "characters:*:get",
            $"characters:{id}:get"
        );
        var character = await _characterServices.GetById(characterid);
        return Ok(character);
    }

    [HttpPut("{id}/characters/{characterid}")]
    public async Task<IActionResult> UpdateCharacter([FromRoute] Guid id, [FromRoute] Guid characterid, [FromBody] UpdateCharacterRequest character)
    {
        RequiredScope(
            "characters:*:update",
            $"characters:{id}:update",
            $"games:{id}:update"
        );
        var updateC = await _characterRepo.FoundOrThrowAsync(characterid, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(character, updateC);
        await _characterServices.Update(updateC);
        return Ok(updateC);
    }

    [HttpDelete("{id}/characters/{characterid}")]
    public async Task<IActionResult> DeleteCharacter([FromRoute] Guid id, [FromRoute] Guid characterid)
    {
        RequiredScope(
            "characters:*:delete",
            $"characters:{id}:delete"
        );
        await _characterRepo.FoundOrThrowAsync(characterid, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _characterServices.Delete(characterid);
        return NoContent();
    }
    #endregion
    #region Character Types
    [HttpGet("{id}/character-types")]
    public async Task<IActionResult> GetCharTypesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "charactertypes:*:get",
            $"charactertypes:{id}:get"
        );
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

    [HttpPost("{id}/character-types")]
    public async Task<IActionResult> CreateCharacterType([FromRoute] Guid id, [FromBody] CreateCharacterTypeRequest charType)
    {
        RequiredScope(
            "charactertypes:create",
            $"charactertypes:{id}:create",
            $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var newCT = new CharacterTypeEntity { GameId = id };
        Mapper.Map(charType, newCT);
        newCT.BaseProperties = charType.BaseProperties.ToString();
        await _characterTypeServices.Create(newCT);
        await UpdateGameRecord(id);
        return CreatedAtAction(nameof(GetCharacterType), new { id = id, charactertypeid = newCT.Id }, newCT);
    }

    [HttpGet("{id}/character-types/{charactertypeid}")]
    public async Task<IActionResult> GetCharacterType([FromRoute] Guid id, [FromRoute] Guid charactertypeid)
    {
        RequiredScope(
            $"games:{id}:get",
            "charactertypes:*:get",
            $"charactertypes:{id}:get"
        );
        var ct = await _characterTypeServices.GetById(charactertypeid);
        var ctResponse = new CharacterTypeResponse();
        Mapper.Map(ct, ctResponse);
        ctResponse.BaseProperties = JsonObject.Parse(ct.BaseProperties);
        return Ok(ctResponse);
    }

    [HttpPut("{id}/character-types/{charactertypeid}")]
    public async Task<IActionResult> UpdateCharacterType([FromRoute] Guid id, [FromRoute] Guid charactertypeid, [FromBody] UpdateCharacterTypeRequest charType)
    {
        RequiredScope(
            "charactertypes:*:update",
            $"charactertypes:{id}:update",
            $"games:{id}:update"
        );
        var ct = await _characterTypeRepo.FoundOrThrowAsync(charactertypeid, Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(charType, ct);
        await _characterTypeServices.Update(ct);
        return Ok(ct);
    }

    [HttpDelete("{id}/character-types/{charactertypeid}")]
    public async Task<IActionResult> DeleteCharacterType([FromRoute] Guid id, [FromRoute] Guid charactertypeid)
    {
        RequiredScope(
            "charactertypes:*:delete",
            $"charactertypes:{id}:delete"
        );
        await _characterTypeRepo.FoundOrThrowAsync(charactertypeid, Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
        await _characterTypeServices.Delete(charactertypeid);
        return NoContent();
    }
    #endregion
    #region Game Servers
    [HttpGet("{id}/game-servers")]
    public async Task<IActionResult> GetGameServersByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "gameservers:*:get",
            $"gameservers:{id}:get"
        );
        return Ok(await _gameServerServices.ListServersByGameId(id));
    }

    [HttpPost("{id}/game-servers")]
    public async Task<IActionResult> CreateGameServer([FromRoute] Guid id, [FromBody] CreateGameServerRequest gameServer)
    {
        RequiredScope(
            "gameservers:create",
            $"gameservers:{id}:create",
            $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var newGameServer = new GameServerEntity { GameId = id };
        Mapper.Map(gameServer, newGameServer);
        await _gameServerServices.Create(newGameServer);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/game-servers/{id}", newGameServer);
    }

    #endregion
    #region Level Progresses
    [HttpGet("{id}/level-progreses")]
    public async Task<IActionResult> GetLevelProgressesByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "levelprogresses:*:get",
            $"levelprogresses:{id}:get"
        );
        return Ok(await _levelProgressServices.ListLevelProgByGameId(id));
    }

    [HttpPost("{id}/level-progreses")]
    public async Task<IActionResult> CreateLevelProgress([FromRoute] Guid id, [FromBody] CreateLevelProgressRequest levelProg)
    {
        RequiredScope(
            "levelprogresses:create",
            $"levelprogresses:{id}:create",
            $"games:{id}:update"
        );
        await _characterRepo.FoundOrThrowAsync(levelProg.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _levelRepo.FoundOrThrowAsync(levelProg.LevelId, Constants.Entities.LEVEL + Constants.Errors.NOT_EXIST_ERROR);
        var newLevelProg = new LevelProgressEntity();
        Mapper.Map(levelProg, newLevelProg);
        await _levelProgressServices.Create(newLevelProg);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/level-progresses/{id}", newLevelProg);
    }

    #endregion
    #region Levels
    [HttpGet("{id}/levels")]
    public async Task<IActionResult> GetLevelsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "levels:*:get",
            $"levels:{id}:get"
        );
        return Ok(await _levelServices.ListLevelsByGameId(id));
    }

    [HttpPost("{id}/levels")]
    public async Task<IActionResult> CreateLevel([FromRoute] Guid id, [FromBody] CreateLevelsRequest[] level)
    {
        RequiredScope(
            "levels:create",
            $"levels:{id}:create",
            $"games:{id}:update"
        );
        List<LevelEntity> levelList = new List<LevelEntity>();
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + "id " + id + " " + Constants.Errors.NOT_EXIST_ERROR);
        foreach (var singleLevel in level)
        {
            LevelEntity newLevel = new LevelEntity
            {
                GameId = id,
                LevelNo = (await _levelRepo.WhereAsync(l => l.GameId == id)).Count() + levelList.Count(l => l.GameId == id) + 1
            };
            Mapper.Map(singleLevel, newLevel);
            levelList.Add(newLevel);
        }
        await _levelServices.Create(levelList);
        await UpdateGameRecord(id, levelList.Count());
        return Created(Constants.Http.API_VERSION + "/gms/levels", levelList);
    }
    #endregion
    #region Payments
    [HttpGet("{id}/payments")]
    public async Task<IActionResult> GetPaymentsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "payments:*:get",
            $"payments:{id}:get"
        );
        return Ok(await _paymentServices.ListPaymentByGameId(id));
    }

    [HttpPost("{id}/payments")]
    public async Task<IActionResult> CreatePayment([FromRoute] Guid id, [FromBody] CreatePaymentRequest payment)
    {
        RequiredScope(
            "payments:create",
            $"payments:{id}:create",
            $"games:{id}:update"
        );
        await _characterRepo.FoundOrThrowAsync(payment.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        await _userRepo.FoundOrThrowAsync(payment.UserId, Constants.Entities.USER + Constants.Errors.NOT_EXIST_ERROR);
        await _walletRepo.FoundOrThrowAsync(payment.WalletId, Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
        var newPayment = new PaymentEntity();
        Mapper.Map(payment, newPayment);
        await _paymentServices.Create(newPayment);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/payments/{id}", newPayment);
    }
    #endregion
    #region Transactions
    [HttpGet("{id}/transactions")]
    public async Task<IActionResult> GetTransactionsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "transactions:*:get",
            $"transactions:{id}:get"
        );
        return Ok(await _transactionServices.ListTransactionsByGameId(id));
    }

    [HttpPost("{id}/transactions")]
    public async Task<IActionResult> CreateTransaction([FromRoute] Guid id, [FromBody] CreateTransactionRequest trans)
    {
        RequiredScope(
            "transactions:create",
            $"transactions:{id}:create",
            $"games:{id}:update"
        );
        await _walletRepo.FoundOrThrowAsync(trans.WalletId, Constants.Entities.WALLET + Constants.Errors.NOT_EXIST_ERROR);
        var newTrans = new TransactionEntity();
        Mapper.Map(trans, newTrans);
        await _transactionServices.Create(newTrans);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/transactions/{id}", newTrans);
    }
    #endregion
    #region Users
    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetUsersByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "users:*:get",
            $"users:{id}:get"
        );
        return Ok(await _gameUserServices.ListUsersByGameId(id));
    }

    [HttpPost("{id}/users")]
    public async Task<IActionResult> CreateUser([FromRoute] Guid id, [FromBody] CreateUserRequest cUser)
    {
        RequiredScope(
            "users:create",
            $"users:{id}:create",
            $"games:{id}:update"
        );
        var user = new UserEntity();
        Mapper.Map(cUser, user);
        await _userServices.Create(user);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/users/{id}", user);
    }

    #endregion
    #region Wallet Categories
    [HttpGet("{id}/wallet-categories")]
    public async Task<IActionResult> GetWalCatsByGameID(Guid id)
    {
        RequiredScope(
            $"games:{id}:get",
            "walletcategories:*:get",
            $"walletcategories:{id}:get"
        );
        return Ok(await _walletCategoryServices.ListWalCatsByGameId(id));
    }

    [HttpPost("{id}/wallet-categories")]
    public async Task<IActionResult> CreateWalletCategory([FromRoute] Guid id, [FromBody] CreateWalletCategoryRequest walCat)
    {
        RequiredScope(
            "walletcategories:create",
            $"walletcategories:{id}:create",
            $"games:{id}:update"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var newWalCat = new WalletCategoryEntity { GameId = id };
        Mapper.Map(walCat, newWalCat);
        await _walletCategoryServices.Create(newWalCat);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/wallet-category/{id}", newWalCat);
    }
    #endregion
    #region Wallets
    [HttpGet("{id}/wallets")]
    public async Task<IActionResult> GetWalletsByGameID(Guid id)
    {
        RequiredScope(
            "wallets:*:get",
            $"wallets:{id}:get",
            $"games:{id}:get"
        );
        return Ok(await _walletServices.ListWalletsByGameId(id));
    }

    [HttpPost("{id}/wallets")]
    public async Task<IActionResult> CreateWallet([FromRoute] Guid id, [FromBody] CreateWalletRequest wallet)
    {
        RequiredScope(
            "wallets:create",
            $"wallets:{id}:create",
            $"games:{id}:update"
        );
        await _walletCategoryRepo.FoundOrThrowAsync(wallet.WalletCategoryId, Constants.Entities.WALLET_CATEGORY + Constants.Errors.NOT_EXIST_ERROR);
        await _characterRepo.FoundOrThrowAsync(wallet.CharacterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);
        var newWallet = new WalletEntity();
        Mapper.Map(wallet, newWallet);
        await _walletServices.Create(newWallet);
        await UpdateGameRecord(id);
        return Created(Constants.Http.API_VERSION + "/gms/wallets/{id}", newWallet);
    }
    #endregion
    [HttpGet("{id}/count-record")]
    public async Task<IActionResult> CountRecordsByGameId(Guid id)
    {
        return Ok(await _gameServices.CountRecord(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest newGame)
    {
        RequiredScope(
            $"games:create"
        );
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
        };
        var reqData = new Dictionary<string, object>
        {
            { "scope", scope },
        };
        return JsonConvert.SerializeObject(reqData);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest game)
    {
        RequiredScope(
            "games:*:update",
            $"games:{id}:update"
        );
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(game, updateGame);
        await _gameServices.Update(updateGame);
        return Ok(updateGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        RequiredScope(
            "games:*:delete",
            $"games:{id}:delete"
        );
        await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        await _gameServices.Delete(id);
        return NoContent();
    }

    [NonAction]
    public async Task UpdateGameRecord(Guid id, int? record = 1)
    {
        var updateGame = await _gameRepo.FoundOrThrowAsync(id, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        updateGame.MonthlyWriteUnits += (int)record;
        await _gameServices.Update(updateGame);
    }
}