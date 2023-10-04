using Domain.Entities;

namespace Application.Interfaces;
public interface ILevelProgressServices {
    Task<ICollection<LevelProgress>> List();
    Task<LevelProgress> GetById(Guid levelProgressId);
    Task<ICollection<LevelProgress>> GetByCharacterId(Guid id);
    Task<ICollection<LevelProgress>> GetByLevelId(Guid id);
    Task<int> Count();
    Task Create(LevelProgress levelProgress);
    Task Update(Guid levelProgressId, LevelProgress levelProgress);
    Task Delete(Guid levelProgressId);
}
