using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AttributeController : BaseController
    {
        private readonly IAttributeServices _attributeServices;
        private readonly IGenericRepository<AttributeGroup> _attributeRepo;
        public AttributeController(IAttributeServices attributeServices, IGenericRepository<AttributeGroup> attributeRepo)
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
        public async Task<IActionResult> Post([FromBody] AttributeGroup attributeGroup)
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
}
