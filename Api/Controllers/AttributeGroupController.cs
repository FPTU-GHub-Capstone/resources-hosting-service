using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using System.Text.Json.Nodes;
using WebApiLayer.UserFeatures.Requests;
using WebApiLayer.UserFeatures.Response;

namespace WebApiLayer.Controllers;

[Route(Constants.Http.API_VERSION + "/gms/games/{gameId}/attribute-groups")]
public class AttributeGroupController : BaseController
{
    private readonly IAttributeGroupServices _attributeServices;
    private readonly IGenericRepository<AttributeGroupEntity> _attributeRepo;
    public AttributeGroupController(IAttributeGroupServices attributeServices, IGenericRepository<AttributeGroupEntity> attributeRepo)
    {
        _attributeServices = attributeServices;
        _attributeRepo = attributeRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAttributeGroups()
    {
        var attributes = await _attributeServices.List();
        List<AttributeGroupResponse> attGrpList = new();
        foreach(var attribute in attributes)
        {
            var attGrp = new AttributeGroupResponse();
            Mapper.Map(attribute, attGrp);
            attGrp.Effect = JsonObject.Parse(attribute.Effect);
            attGrpList.Add(attGrp);
        }
        return Ok(attGrpList);
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