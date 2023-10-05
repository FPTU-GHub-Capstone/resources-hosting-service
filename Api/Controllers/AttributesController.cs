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
    public async Task<IActionResult> Get()
    {
        var attribute = await _attributeServices.List();
        return Ok(attribute);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var attribute = _attributeServices.GetById(Guid.Parse(id));
        return Ok(attribute.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AttributeGroupEntity attributeGroup)
    {
        await _attributeServices.Create(attributeGroup);
        return NoContent();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
