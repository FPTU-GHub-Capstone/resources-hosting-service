using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
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
    public AttributeGroupController(IAttributeGroupServices attributeServices)
    {
        _attributeServices = attributeServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetAttributeGroups()
    {
        RequiredScope("games:*:get", "attributegroups:*:get");
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
}