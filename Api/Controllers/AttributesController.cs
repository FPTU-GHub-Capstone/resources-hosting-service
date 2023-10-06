using DomainLayer.Constants;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Repositories;
using ServiceLayer.Business;

namespace WebApiLayer.Controllers;

[Route(Constants.HTTP.API_VERSION + "/gms/attributes")]
public class AttributesController : BaseController
{
    private readonly IAttributeGroupServices _attributeServices;
    private readonly IGenericRepository<AttributeGroupEntity> _attributeRepo;
    public AttributesController(IAttributeGroupServices attributeServices, IGenericRepository<AttributeGroupEntity> attributeRepo)
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
    public async Task<IActionResult> GetAttributeGroup(string id)
    {
        var attribute = _attributeServices.GetById(Guid.Parse(id));
        return Ok(attribute.Result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttributeGroup([FromBody] AttributeGroupEntity attributeGroup)
    {
        await _attributeServices.Create(attributeGroup);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeGroup(string id, [FromBody] AttributeGroupEntity attributeGroup)
    {
        await _attributeServices.Update(Guid.Parse(id),attributeGroup);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _attributeServices.Delete(Guid.Parse(id));
        return Ok();
    }
}
