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

[Route(Constants.Http.API_VERSION + "/gms/attribute-groups")]
public class AttributeGroupController : BaseController
{
    private readonly IAttributeGroupServices _attributeServices;
    private readonly IGenericRepository<AttributeGroupEntity> _attributeRepo;
    private readonly IGenericRepository<GameEntity> _gameRepo;
    public AttributeGroupController(IAttributeGroupServices attributeServices, IGenericRepository<AttributeGroupEntity> attributeRepo
        , IGenericRepository<GameEntity> gameRepo)
    {
        _attributeServices = attributeServices;
        _attributeRepo = attributeRepo;
        _gameRepo = gameRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAttributeGroups()
    {
        if (CurrentScp.Contains("attributegroups:*:get"))
        {
            var attributes = await _attributeServices.List();
            List<AttributeGroupResponse> attGrpList = new();
            foreach (var attribute in attributes)
            {
                var attGrp = new AttributeGroupResponse();
                Mapper.Map(attribute, attGrp);
                attGrp.Effect = JsonObject.Parse(attribute.Effect);
                attGrpList.Add(attGrp);
            }
            return Ok(attGrpList);
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttributeGroup(Guid id)
    {
        var attribute = await _attributeServices.GetById(id);
        return Ok(attribute);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttributeGroup([FromBody] CreateAttributeGroupRequest attributeGroup)
    {
        await _gameRepo.FoundOrThrowAsync(attributeGroup.GameId, Constants.Entities.GAME + Constants.Errors.NOT_EXIST_ERROR);
        var attGrpEnt = new AttributeGroupEntity();
        Mapper.Map(attributeGroup, attGrpEnt);
        attGrpEnt.Effect = attributeGroup.Effect.ToString();
        await _attributeServices.Create(attGrpEnt);
        return CreatedAtAction(nameof(GetAttributeGroup), new { id = attGrpEnt.Id }, attGrpEnt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeGroup(Guid id, [FromBody] UpdateAttributeGroupRequest attributeGroup)
    {
        var attGrpEnt = await _attributeRepo.FoundOrThrowAsync(id, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        Mapper.Map(attributeGroup, attGrpEnt);
        await _attributeServices.Update(attGrpEnt);
        return Ok(attGrpEnt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeGroup(Guid id)
    {
        await _attributeRepo.FoundOrThrowAsync(id, Constants.Entities.ATTRIBUTE_GROUP + Constants.Errors.NOT_EXIST_ERROR);
        await _attributeServices.Delete(id);
        return NoContent();
    }
}