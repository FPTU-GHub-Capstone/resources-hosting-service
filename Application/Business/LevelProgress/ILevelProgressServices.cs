using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ILevelProgressServices
{
    Task<ICollection<LevelProgressEntity>> List();
    Task<LevelProgressEntity> GetById(Guid levelProgressId);
    Task Create(LevelProgressEntity levelProgress);
    Task Update(LevelProgressEntity levelProgress);
    Task Delete(Guid levelProgressId);
}
