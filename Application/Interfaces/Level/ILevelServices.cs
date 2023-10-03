using Domain.Entities.Level;

namespace Application.Interfaces; 
public interface ILevelServices {
    //Level
    Task<ICollection<LevelEntity>> List();
    Task<LevelEntity> GetById(Guid levelId);
    Task<ICollection<LevelEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task<int> Count(Guid GameId); // Count levels in 1 game
    Task Create(LevelEntity level);
    Task Update(Guid levelId, LevelEntity level);
    Task Delete(Guid levelId);
}
