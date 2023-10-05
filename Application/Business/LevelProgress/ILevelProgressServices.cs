using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ILevelProgressServices
{
    Task<ICollection<LevelProgressEntity>> List();
    Task<LevelProgressEntity> GetById(Guid levelProgressId);
    Task<ICollection<LevelProgressEntity>> GetByCharacterId(Guid id);
    Task<ICollection<LevelProgressEntity>> GetByLevelId(Guid id);
    Task<int> Count();
    Task Create(LevelProgressEntity levelProgress);
    Task Update(Guid levelProgressId, LevelProgressEntity levelProgress);
    Task Delete(Guid levelProgressId);
}
