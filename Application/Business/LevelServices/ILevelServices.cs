using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ILevelServices
{
    Task<ICollection<LevelEntity>> List();
    Task<LevelEntity> GetById(Guid levelId);
    Task<ICollection<LevelEntity>> List(Guid[] levelIds);
    Task<ICollection<LevelEntity>> ListLevelsByGameId(Guid gameId);
    Task Create(List<LevelEntity> level);
    Task Update(LevelEntity level);
    Task Delete(LevelEntity level);
}
