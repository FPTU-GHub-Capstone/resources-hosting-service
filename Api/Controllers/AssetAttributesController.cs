using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/asset-attributes")]
public class AssetAttributesController : BaseController
{
    private readonly IAssetAttributeServices _assetAttServices;
    private readonly IGenericRepository<AssetAttributeEntity> _assetAttRepo;
    private readonly IGenericRepository<AssetEntity> _assetRepo;
    private readonly IGenericRepository<AttributeGroupEntity> _attGrpRepo;
    public AssetAttributesController(IAssetAttributeServices assetAttServices, IGenericRepository<AssetAttributeEntity> assetAttRepo
        , IGenericRepository<AssetEntity> assetRepo, IGenericRepository<AttributeGroupEntity> attGrpRepo)
    {
        _assetAttServices = assetAttServices;
        _assetAttRepo = assetAttRepo;
        _assetRepo = assetRepo;
        _attGrpRepo = attGrpRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetAttributes()
    {
        return Ok(await _assetAttServices.List());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssetAttribute(Guid id)
    {
        return Ok(await _assetAttServices.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssetAttribute([FromBody] CreateAssetAttributeRequest assetAtt)
    {
        await _assetRepo.FoundOrThrowAsync(assetAtt.AssetId, Constants.ENTITY.ASSET + Constants.ERROR.NOT_EXIST_ERROR);
        await _attGrpRepo.FoundOrThrowAsync(assetAtt.AttributeGroupId, Constants.ENTITY.ATTRIBUTE_GROUP + Constants.ERROR.NOT_EXIST_ERROR);
        var newAssAtt = new AssetAttributeEntity();
        Mapper.Map(assetAtt, newAssAtt);
        await _assetAttServices.Create(newAssAtt);
        return CreatedAtAction(nameof(GetAssetAttribute), new { id = newAssAtt.Id }, newAssAtt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssetAttribute(Guid id, [FromBody] UpdateAssetAttributeRequest assetAtt)
    {
        var updateAssAtt = await _assetAttRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET_ATTRIBUTE + Constants.ERROR.NOT_EXIST_ERROR);
        Mapper.Map(assetAtt, updateAssAtt);
        await _assetAttServices.Update(updateAssAtt);
        return Ok(updateAssAtt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssetAttribute(Guid id)
    {
        await _assetAttRepo.FoundOrThrowAsync(id, Constants.ENTITY.ASSET_ATTRIBUTE + Constants.ERROR.NOT_EXIST_ERROR);
        await _assetAttServices.Delete(id);
        return NoContent();
    }
}
