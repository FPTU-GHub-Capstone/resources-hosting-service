using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAttributeGroupServices
{
    Task<ICollection<AttributeGroupEntity>> List();
    Task<AttributeGroupEntity> GetById(Guid attributeGroupid);
    Task<int> Count();
    Task Create(AttributeGroupEntity attributeGroup);
    Task Update(Guid attributeGroupid, AttributeGroupEntity attributeGroup);
    Task Delete(Guid attributeGroupid);
}
