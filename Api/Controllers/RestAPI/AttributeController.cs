using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Attribute;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeServices _attributeServices;
        private readonly IGenericRepository<AttributeGroup> _attributeRepo;
        public AttributeController(IAttributeServices attributeServices, IGenericRepository<AttributeGroup> attributeRepo)
        {
            _attributeServices = attributeServices;
            _attributeRepo = attributeRepo;
        }
        // GET: api/<AttributeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var attribute = await _attributeServices.GetAttributeGroups();
            if (attribute.Any())
            {
                return Ok(attribute);
            }
            else
            {
                throw new Exception("0 item in list");
            }
        }

        // GET api/<AttributeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttributeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AttributeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AttributeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
