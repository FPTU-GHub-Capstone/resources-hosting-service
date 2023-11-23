using DomainLayer.Entities;

namespace ServiceLayer.Business;
public interface ILevelServices
{
    Task<ICollection<LevelEntity>> List();
    Task<LevelEntity> GetById(Guid levelId);
    Task<ICollection<LevelEntity>> List(Guid[] levelIds);
    Task<ICollection<LevelEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task<int> Count(Guid GameId); // Count levels in 1 game
    Task Create(List<LevelEntity> level);
    Task Update(LevelEntity level);
    Task Delete(Guid levelId);
    Task CheckForDuplicateLevel(string name, Guid GameId, Guid? id = null);
}
