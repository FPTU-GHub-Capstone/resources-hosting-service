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

    protected void RequiredScope(params string[] scope)
    {
        foreach (var scp in scope)
        {
            if (CurrentScp.Contains(scp))
            {
                return;
            }
        }
        throw new ForbiddenException();
    }

    #region Activity Permission
    protected void CheckGetActivityPermission(Guid gameId)
    {

        if (!CurrentScp.Contains("activities:*:get") && !CurrentScp.Contains($"activities:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateActivityPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activities:create") && !CurrentScp.Contains($"activities:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateActivityPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activities:*:update") && !CurrentScp.Contains($"activities:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteActivityPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activities:*:delete") && !CurrentScp.Contains($"activities:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Activity Type Permission
    protected void CheckGetActivityTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activitytypes:*:get") && !CurrentScp.Contains($"activitytypes:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateActivityTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activitytypes:create") && !CurrentScp.Contains($"activitytypes:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateActivityTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activitytypes:*:update") && !CurrentScp.Contains($"activitytypes:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckDeleteActivityTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("activitytypes:*:delete") && !CurrentScp.Contains($"activitytypes:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Asset Attribute Permission
    protected void CheckGetAssetAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assetattributes:*:get") && !CurrentScp.Contains($"assetattributes:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAssetAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assetattributes:create") && !CurrentScp.Contains($"assetattributes:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAssetAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assetattributes:*:update") && !CurrentScp.Contains($"assetattributes:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAssetAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assetattributes:*:delete") && !CurrentScp.Contains($"assetattributes:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Asset Permission
    protected void CheckGetAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assets:*:get") && !CurrentScp.Contains($"assets:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assets:create") && !CurrentScp.Contains($"assets:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assets:*:update") && !CurrentScp.Contains($"assets:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assets:*:delete") && !CurrentScp.Contains($"assets:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Asset Type Permission
    protected void CheckGetAssetTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assettypes:*:get") && !CurrentScp.Contains($"assettypes:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAssetTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assettypes:create") && !CurrentScp.Contains($"assettypes:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAssetTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assettypes:*:update") && !CurrentScp.Contains($"assettypes:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAssetTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("assettypes:*:delete") && !CurrentScp.Contains($"assettypes:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Attribute Group Permission
    protected void CheckGetAttributeGroupPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("attributegroups:*:get") && !CurrentScp.Contains($"attributegroups:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateAttributeGroupPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("attributegroups:create") && !CurrentScp.Contains($"attributegroups:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateAttributeGroupPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("attributegroups:*:update") && !CurrentScp.Contains($"attributegroups:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteAttributeGroupPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("attributegroups:*:delete") && !CurrentScp.Contains($"attributegroups:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Asset Permission
    protected void CheckGetCharacterAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterassets:*:get") && !CurrentScp.Contains($"characterassets:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterassets:create") && !CurrentScp.Contains($"characterassets:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterassets:*:update") && !CurrentScp.Contains($"characterassets:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterAssetPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterassets:*:delete") && !CurrentScp.Contains($"characterassets:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Attribute Permission
    protected void CheckGetCharacterAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterattributes:*:get") && !CurrentScp.Contains($"characterattributes:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterattributes:create") && !CurrentScp.Contains($"characterattributes:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterattributes:*:update") && !CurrentScp.Contains($"characterattributes:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterAttributePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characterattributes:*:delete") && !CurrentScp.Contains($"characterattributes:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Permission
    protected void CheckGetCharacterPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characters:*:get") && !CurrentScp.Contains($"characters:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characters:create") && !CurrentScp.Contains($"characters:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characters:*:update") && !CurrentScp.Contains($"characters:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("characters:*:delete") && !CurrentScp.Contains($"characters:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Character Type Permission
    protected void CheckGetCharacterTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("charactertypes:*:get") && !CurrentScp.Contains($"charactertypes:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateCharacterTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("charactertypes:create") && !CurrentScp.Contains($"charactertypes:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateCharacterTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("charactertypes:*:update") && !CurrentScp.Contains($"charactertypes:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteCharacterTypePermission(Guid gameId)
    {
        if (!CurrentScp.Contains("charactertypes:*:delete") && !CurrentScp.Contains($"charactertypes:{gameId}:delete"))
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
    protected void CheckGetGameServerPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("gameservers:*:get") && !CurrentScp.Contains($"gameservers:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateGameServerPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("gameservers:create") && !CurrentScp.Contains($"gameservers:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateGameServerPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("gameservers:*:update") && !CurrentScp.Contains($"gameservers:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteGameServerPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("gameservers:*:delete") && !CurrentScp.Contains($"gameservers:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Level Progress Permission
    protected void CheckGetLevelProgressPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:get") && !CurrentScp.Contains($"levelprogresses:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateLevelProgressPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levelprogresses:create") && !CurrentScp.Contains($"levelprogresses:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateLevelProgressPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:update") && !CurrentScp.Contains($"levelprogresses:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteLevelProgressPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levelprogresses:*:delete") && !CurrentScp.Contains($"levelprogresses:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Level Permission
    protected void CheckGetLevelPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levels:*:get") && !CurrentScp.Contains($"levels:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateLevelPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levels:create") && !CurrentScp.Contains($"levels:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateLevelPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levels:*:update") && !CurrentScp.Contains($"levels:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteLevelPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("levels:*:delete") && !CurrentScp.Contains($"levels:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Payment Permission
    protected void CheckGetPaymentPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("payments:*:get") && !CurrentScp.Contains($"payments:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreatePaymentPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("payments:create") && !CurrentScp.Contains($"payments:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdatePaymentPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("payments:*:update") && !CurrentScp.Contains($"payments:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeletePaymentPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("payments:*:delete") && !CurrentScp.Contains($"payments:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Transaction Permission
    protected void CheckGetTransactionPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("transactions:*:get") && !CurrentScp.Contains($"transactions:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateTransactionPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("transactions:create") && !CurrentScp.Contains($"transactions:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateTransactionPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("transactions:*:update") && !CurrentScp.Contains($"transactions:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteTransactionPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("transactions:*:delete") && !CurrentScp.Contains($"transactions:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region User Permission
    protected void CheckGetUserPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("users:*:get") && !CurrentScp.Contains($"users:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateUserPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("users:create") && !CurrentScp.Contains($"users:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateUserPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("users:*:update") && !CurrentScp.Contains($"users:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteUserPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("users:*:delete") && !CurrentScp.Contains($"users:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Wallet Category Permission
    protected void CheckGetWalletCategoryPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("walletcategories:*:get") && !CurrentScp.Contains($"walletcategories:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateWalletCategoryPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("walletcategories:create") && !CurrentScp.Contains($"walletcategories:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateWalletCategoryPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("walletcategories:*:update") && !CurrentScp.Contains($"walletcategories:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteWalletCategoryPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("walletcategories:*:delete") && !CurrentScp.Contains($"walletcategories:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
    #region Wallet Permission
    protected void CheckGetWalletPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("wallets:*:get") && !CurrentScp.Contains($"wallets:{gameId}:get"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckCreateWalletPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("wallets:create") && !CurrentScp.Contains($"wallets:{gameId}:create")
            && !CurrentScp.Contains($"games:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }
    protected void CheckUpdateWalletPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("wallets:*:update") && !CurrentScp.Contains($"wallets:{gameId}:update"))
        {
            throw new ForbiddenException();
        }
    }

    protected void CheckDeleteWalletPermission(Guid gameId)
    {
        if (!CurrentScp.Contains("wallets:*:delete") && !CurrentScp.Contains($"wallets:{gameId}:delete"))
        {
            throw new ForbiddenException();
        }
    }
    #endregion
}
