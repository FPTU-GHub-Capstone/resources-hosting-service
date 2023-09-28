using Domain.Entities.Attribute;

namespace Application.Interfaces;

public interface IAttributeServices
{
    //Attribute Group
    Task<ICollection<AttributeGroup>> GetAttributeGroups();
    Task<AttributeGroup> GetAttributeGroup(Guid attributeGroupid);
    Task<int> CountAttributeGroups();
    Task CreateAttributeGroup(AttributeGroup attributeGroup);
    Task UpdateAttributeGroup(Guid attributeGroupid, AttributeGroup attributeGroup);
    Task DeleteAttributeGroup(Guid attributeGroupid);
}
