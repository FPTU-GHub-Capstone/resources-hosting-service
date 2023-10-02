using Application.Interfaces;
using Domain.Entities.Activity;
using Domain.Entities.Attribute;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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

        // GET: api/<AttributeController>/Count
        [Route("Count")]
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var count = await _attributeServices.CountAttributeGroups();
            return Ok(count);
        }

        // GET api/<AttributeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var attribute = _attributeServices.GetAttributeGroup(Guid.Parse(id));
                return Ok(attribute.Result);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<AttributeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AttributeGroup attributeGroup)
        {
            try
            {
                attributeGroup.Id = Guid.NewGuid();
                attributeGroup.isDeleted = false;
                var attGrps = _attributeServices.GetAttributeGroups();
                foreach(var attgrp in attGrps.Result)
                {
                    if(attgrp.Name == attributeGroup.Name)
                    {
                        return Conflict(attgrp);
                    } else
                    continue;
                }
                await _attributeServices.CreateAttributeGroup(attributeGroup);
                return NoContent();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
