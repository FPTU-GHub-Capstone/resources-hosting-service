using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;
using WebApiLayer.UserFeatures.Requests;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/attribute-groups")]
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
        var attribute = await _attributeServices.List();
        return Ok(attribute);
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
        Mapper.Map(attributeGroup,attGrpEnt);
        await _attributeServices.Create(attGrpEnt);
        return CreatedAtAction(nameof(GetAttributeGroup), new { id = attGrpEnt.Id }, attGrpEnt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeGroup(Guid id, [FromBody] UpdateAttributeGroupRequest attributeGroup)
    {
        var attGrpEnt = await _attributeServices.GetById(id);
        Mapper.Map(attributeGroup, attGrpEnt);
        await _attributeServices.Update(id, attGrpEnt);
        return Ok(attGrpEnt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _attributeServices.Delete(id);
        return NoContent();
    }
}
