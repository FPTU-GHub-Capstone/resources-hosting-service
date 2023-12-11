using AutoMapper;
using DomainLayer.Constants;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WebApiLayer.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected string CurrentUid => GetCurrentUid();

    protected string CurrentSid => GetCurrentSid();

    protected string[] CurrentScp => GetCurrentScp();

    protected string CurrentToken => GetCurrentToken();

    protected string GetCurrentToken()
    {
        const string bearerPrefix = "Bearer ";
        var token = Request.Headers["Authorization"].ToString();
        return token.Substring(bearerPrefix.Length);
    }

    protected string GetCurrentUid()
    {
        var uid = HttpContext.User.Claims.FirstOrDefault(a => a.Type == Constants.HttpContext.UID) ?? throw new ForbiddenException();
        return uid.Value;
    }

    protected string GetCurrentSid()
    {
        var sid = HttpContext.User.Claims.FirstOrDefault(a => a.Type == Constants.HttpContext.SID) ?? throw new ForbiddenException();
        return sid.Value;
    }

    protected string[] GetCurrentScp()
    {
        var scp = HttpContext.User.Claims.Where(a => a.Type == Constants.HttpContext.SCP)
                .Select(scope => scope.Value).ToArray();
        return scp;
    }

    protected async Task<T> BuildJsonResponse<T>(HttpResponseMessage response)
    {
        var stringData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringData); ;
    }
    #region Activity Permission
    protected void CheckGetActivityPermission(Guid activityId)
    {
        if (!CurrentScp.Contains("activities:*:get") && !CurrentScp.Contains($"activities:{activityId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateActivityPermission(Guid activityId)
    {
        if (!CurrentScp.Contains("activities:*:create") && !CurrentScp.Contains($"activities:{activityId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateActivityPermission(Guid activityId)
    {
        if (!CurrentScp.Contains("activities:*:update") && !CurrentScp.Contains($"activities:{activityId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteActivityPermission(Guid activityId)
    {
        if (!CurrentScp.Contains("activities:*:delete") && !CurrentScp.Contains($"activities:{activityId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Activity Type Permission
    protected void CheckGetActivityTypePermission(Guid activityTypeId)
    {
        if (!CurrentScp.Contains("activitytypes:*:get") && !CurrentScp.Contains($"activitytypes:{activityTypeId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateActivityTypePermission(Guid activityTypeId)
    {
        if (!CurrentScp.Contains("activitytypes:*:create") && !CurrentScp.Contains($"activitytypes:{activityTypeId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateActivityTypePermission(Guid activityTypeId)
    {
        if (!CurrentScp.Contains("activitytypes:*:update") && !CurrentScp.Contains($"activitytypes:{activityTypeId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteActivityTypePermission(Guid activityTypeId)
    {
        if (!CurrentScp.Contains("activitytypes:*:delete") && !CurrentScp.Contains($"activitytypes:{activityTypeId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Asset Attribute Permission
    protected void CheckGetAssetAttributePermission(Guid assetAttributeId)
    {
        if (!CurrentScp.Contains("assetattributes:*:get") && !CurrentScp.Contains($"assetattributes:{assetAttributeId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAssetAttributePermission(Guid assetAttributeId)
    {
        if (!CurrentScp.Contains("assetattributes:*:create") && !CurrentScp.Contains($"assetattributes:{assetAttributeId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAssetAttributePermission(Guid assetAttributeId)
    {
        if (!CurrentScp.Contains("assetattributes:*:update") && !CurrentScp.Contains($"assetattributes:{assetAttributeId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAssetAttributePermission(Guid assetAttributeId)
    {
        if (!CurrentScp.Contains("assetattributes:*:delete") && !CurrentScp.Contains($"assetattributes:{assetAttributeId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Asset Permission
    protected void CheckGetAssetPermission(Guid assetId)
    {
        if (!CurrentScp.Contains("assets:*:get") && !CurrentScp.Contains($"assets:{assetId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAssetPermission(Guid assetId)
    {
        if (!CurrentScp.Contains("assets:*:create") && !CurrentScp.Contains($"assets:{assetId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAssetPermission(Guid assetId)
    {
        if (!CurrentScp.Contains("assets:*:update") && !CurrentScp.Contains($"assets:{assetId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAssetPermission(Guid assetId)
    {
        if (!CurrentScp.Contains("assets:*:delete") && !CurrentScp.Contains($"assets:{assetId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Attribute Group Permission
    protected void CheckGetAttributeGroupPermission(Guid attributeGroupId)
    {
        if (!CurrentScp.Contains("attributegroups:*:get") && !CurrentScp.Contains($"attributegroups:{attributeGroupId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAttributeGroupPermission(Guid attributeGroupId)
    {
        if (!CurrentScp.Contains("attributegroups:*:create") && !CurrentScp.Contains($"attributegroups:{attributeGroupId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAttributeGroupPermission(Guid attributeGroupId)
    {
        if (!CurrentScp.Contains("attributegroups:*:update") && !CurrentScp.Contains($"attributegroups:{attributeGroupId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAttributeGroupPermission(Guid attributeGroupId)
    {
        if (!CurrentScp.Contains("attributegroups:*:delete") && !CurrentScp.Contains($"attributegroups:{attributeGroupId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Asset Permission
    protected void CheckGetCharacterAssetPermission(Guid characterAssetId)
    {
        if (!CurrentScp.Contains("characterassets:*:get") && !CurrentScp.Contains($"characterassets:{characterAssetId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterAssetPermission(Guid characterAssetId)
    {
        if (!CurrentScp.Contains("characterassets:*:create") && !CurrentScp.Contains($"characterassets:{characterAssetId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterAssetPermission(Guid characterAssetId)
    {
        if (!CurrentScp.Contains("characterassets:*:update") && !CurrentScp.Contains($"characterassets:{characterAssetId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterAssetPermission(Guid characterAssetId)
    {
        if (!CurrentScp.Contains("characterassets:*:delete") && !CurrentScp.Contains($"characterassets:{characterAssetId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Attribute Permission
    protected void CheckGetCharacterAttributePermission(Guid characterAttributeId)
    {
        if (!CurrentScp.Contains("characterattributes:*:get") && !CurrentScp.Contains($"characterattributes:{characterAttributeId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterAttributePermission(Guid characterAttributeId)
    {
        if (!CurrentScp.Contains("characterattributes:*:create") && !CurrentScp.Contains($"characterattributes:{characterAttributeId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterAttributePermission(Guid characterAttributeId)
    {
        if (!CurrentScp.Contains("characterattributes:*:update") && !CurrentScp.Contains($"characterattributes:{characterAttributeId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterAttributePermission(Guid characterAttributeId)
    {
        if (!CurrentScp.Contains("characterattributes:*:delete") && !CurrentScp.Contains($"characterattributes:{characterAttributeId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Permission
    protected void CheckGetCharacterPermission(Guid characterId)
    {
        if (!CurrentScp.Contains("characters:*:get") && !CurrentScp.Contains($"characters:{characterId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterPermission(Guid characterId)
    {
        if (!CurrentScp.Contains("characters:*:create") && !CurrentScp.Contains($"characters:{characterId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterPermission(Guid characterId)
    {
        if (!CurrentScp.Contains("characters:*:update") && !CurrentScp.Contains($"characters:{characterId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterPermission(Guid characterId)
    {
        if (!CurrentScp.Contains("characters:*:delete") && !CurrentScp.Contains($"characters:{characterId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Type Permission
    protected void CheckGetCharacterTypePermission(Guid characterTypeId)
    {
        if (!CurrentScp.Contains("charactertypes:*:get") && !CurrentScp.Contains($"charactertypes:{characterTypeId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterTypePermission(Guid characterTypeId)
    {
        if (!CurrentScp.Contains("charactertypes:*:create") && !CurrentScp.Contains($"charactertypes:{characterTypeId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterTypePermission(Guid characterTypeId)
    {
        if (!CurrentScp.Contains("charactertypes:*:update") && !CurrentScp.Contains($"charactertypes:{characterTypeId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterTypePermission(Guid characterTypeId)
    {
        if (!CurrentScp.Contains("charactertypes:*:delete") && !CurrentScp.Contains($"charactertypes:{characterTypeId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Game Permission
    protected void CheckGetGamePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("games:*:get") && !CurrentScp.Contains($"games:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckUpdateGamePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("games:*:update") && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteGamePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("games:*:delete") && !CurrentScp.Contains($"games:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Game Server Permission
    protected void CheckGetGameServerPermission(Guid gameServerId)
    {
        if (!CurrentScp.Contains("gameservers:*:get") && !CurrentScp.Contains($"gameservers:{gameServerId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateGameServerPermission(Guid gameServerId)
    {
        if (!CurrentScp.Contains("gameservers:*:create") && !CurrentScp.Contains($"gameservers:{gameServerId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateGameServerPermission(Guid gameServerId)
    {
        if (!CurrentScp.Contains("gameservers:*:update") && !CurrentScp.Contains($"gameservers:{gameServerId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteGameServerPermission(Guid gameServerId)
    {
        if (!CurrentScp.Contains("gameservers:*:delete") && !CurrentScp.Contains($"gameservers:{gameServerId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Level Progress Permission
    protected void CheckGetLevelProgressPermission(Guid levelprogressId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:get") && !CurrentScp.Contains($"levelprogresses:{levelprogressId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateLevelProgressPermission(Guid levelprogressId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:create") && !CurrentScp.Contains($"levelprogresses:{levelprogressId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateLevelProgressPermission(Guid levelprogressId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:update") && !CurrentScp.Contains($"levelprogresses:{levelprogressId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteLevelProgressPermission(Guid levelprogressId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:delete") && !CurrentScp.Contains($"levelprogresses:{levelprogressId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Level Permission
    protected void CheckGetLevelPermission(Guid levelId)
    {
        if (!CurrentScp.Contains("levels:*:get") && !CurrentScp.Contains($"levels:{levelId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateLevelPermission(Guid levelId)
    {
        if (!CurrentScp.Contains("levels:*:create") && !CurrentScp.Contains($"levels:{levelId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateLevelPermission(Guid levelId)
    {
        if (!CurrentScp.Contains("levels:*:update") && !CurrentScp.Contains($"levels:{levelId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteLevelPermission(Guid levelId)
    {
        if (!CurrentScp.Contains("levels:*:delete") && !CurrentScp.Contains($"levels:{levelId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Payment Permission
    protected void CheckGetPaymentPermission(Guid paymentId)
    {
        if (!CurrentScp.Contains("payments:*:get") && !CurrentScp.Contains($"payments:{paymentId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreatePaymentPermission(Guid paymentId)
    {
        if (!CurrentScp.Contains("payments:*:create") && !CurrentScp.Contains($"payments:{paymentId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdatePaymentPermission(Guid paymentId)
    {
        if (!CurrentScp.Contains("payments:*:update") && !CurrentScp.Contains($"payments:{paymentId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeletePaymentPermission(Guid paymentId)
    {
        if (!CurrentScp.Contains("payments:*:delete") && !CurrentScp.Contains($"payments:{paymentId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Transaction Permission
    protected void CheckGetTransactionPermission(Guid transactionId)
    {
        if (!CurrentScp.Contains("transactions:*:get") && !CurrentScp.Contains($"transactions:{transactionId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateTransactionPermission(Guid transactionId)
    {
        if (!CurrentScp.Contains("transactions:*:create") && !CurrentScp.Contains($"transactions:{transactionId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateTransactionPermission(Guid transactionId)
    {
        if (!CurrentScp.Contains("transactions:*:update") && !CurrentScp.Contains($"transactions:{transactionId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteTransactionPermission(Guid transactionId)
    {
        if (!CurrentScp.Contains("transactions:*:delete") && !CurrentScp.Contains($"transactions:{transactionId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region User Permission
    protected void CheckGetUserPermission(Guid userId)
    {
        if (!CurrentScp.Contains("users:*:get") && !CurrentScp.Contains($"users:{userId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateUserPermission(Guid userId)
    {
        if (!CurrentScp.Contains("users:*:create") && !CurrentScp.Contains($"users:{userId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateUserPermission(Guid userId)
    {
        if (!CurrentScp.Contains("users:*:update") && !CurrentScp.Contains($"users:{userId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteUserPermission(Guid userId)
    {
        if (!CurrentScp.Contains("users:*:delete") && !CurrentScp.Contains($"users:{userId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Wallet Category Permission
    protected void CheckGetWalletCategoryPermission(Guid walletCategoryId)
    {
        if (!CurrentScp.Contains("walletcategories:*:get") && !CurrentScp.Contains($"walletcategories:{walletCategoryId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateWalletCategoryPermission(Guid walletCategoryId)
    {
        if (!CurrentScp.Contains("walletcategories:*:create") && !CurrentScp.Contains($"walletcategories:{walletCategoryId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateWalletCategoryPermission(Guid walletCategoryId)
    {
        if (!CurrentScp.Contains("walletcategories:*:update") && !CurrentScp.Contains($"walletcategories:{walletCategoryId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteWalletCategoryPermission(Guid walletCategoryId)
    {
        if (!CurrentScp.Contains("walletcategories:*:delete") && !CurrentScp.Contains($"walletcategories:{walletCategoryId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Wallet Permission
    protected void CheckGetWalletPermission(Guid walletId)
    {
        if (!CurrentScp.Contains("wallets:*:get") && !CurrentScp.Contains($"wallets:{walletId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateWalletPermission(Guid walletId)
    {
        if (!CurrentScp.Contains("wallets:*:create") && !CurrentScp.Contains($"wallets:{walletId}:create"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateWalletPermission(Guid walletId)
    {
        if (!CurrentScp.Contains("wallets:*:update") && !CurrentScp.Contains($"wallets:{walletId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteWalletPermission(Guid walletId)
    {
        if (!CurrentScp.Contains("wallets:*:delete") && !CurrentScp.Contains($"wallets:{walletId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
}
