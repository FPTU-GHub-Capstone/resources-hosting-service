using Domain.Entities.Level;

namespace Application.Interfaces; 
public interface ILevelProgressServices {
    //Level Progress
    Task<ICollection<LevelProgress>> List();
    Task<LevelProgress> GetById(Guid levelProgressId);
    Task<ICollection<LevelProgress>> GetById(Guid id, int typeId); // TypeID: 1: CharacterId, 2: LevelId
    Task<int> Count();
    Task Create(LevelProgress levelProgress);
    Task Update(Guid levelProgressId, LevelProgress levelProgress);
    Task Delete(Guid levelProgressId);
}
