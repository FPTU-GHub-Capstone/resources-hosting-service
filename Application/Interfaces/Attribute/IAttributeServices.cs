using Domain.Entities.Attribute;

namespace Application.Interfaces;

public interface IAttributeServices
{
    Task<ICollection<AttributeGroup>> List();
    Task<AttributeGroup> GetById(Guid attributeGroupid);
    Task<int> Count();
    Task Create(AttributeGroup attributeGroup);
    Task Update(Guid attributeGroupid, AttributeGroup attributeGroup);
    Task Delete(Guid attributeGroupid);
}
