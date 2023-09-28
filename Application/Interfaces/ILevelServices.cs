using Domain.Entities.Level;

namespace Application.Interfaces; 
public interface ILevelServices {
    //Level
    Task<ICollection<LevelEntity>> GetLevels();
    Task<LevelEntity> GetLevel(Guid levelId);
    Task<ICollection<LevelEntity>> GetLevels(Guid gameId);
    Task<int> CountLevels();
    Task<int> CountLevels(Guid GameId); // Count levels in 1 game
    Task CreateLevel(LevelEntity level);
    Task UpdateLevel(Guid levelId, LevelEntity level);
    Task DeleteLevel(Guid levelId);
    //Level Progress
    Task<ICollection<LevelProgress>> GetLevelProgresses();
    Task<LevelProgress> GetLevelProgress(Guid levelProgressId);
    Task<ICollection<LevelProgress>> GetLevelProgresses(Guid id, int typeId); // TypeID: 1: CharacterId, 2: LevelId
    Task<int> CountLevelProgresses();
    Task CreateLevelProgress(LevelProgress levelProgress);
    Task UpdateLevelProgress(Guid levelProgressId, LevelProgress levelProgress);
    Task DeleteLevelProgress(Guid levelProgressId);
}
