using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterServices : ICharacterServices
{
    private readonly IGenericRepository<CharacterEntity> _characterRepo;
    private readonly IGameServerServices _gameServerService;
    private readonly ICharacterTypeServices _characterTypeService;
    private readonly IGameUserServices _gameUserService;

    public CharacterServices(IGenericRepository<CharacterEntity> characterRepo, IGameServerServices gameServerService
        , ICharacterTypeServices characterTypeService, IGameUserServices gameUserService)
    {
        _characterRepo = characterRepo;
        _gameServerService = gameServerService;
        _characterTypeService = characterTypeService;
        _gameUserService = gameUserService;
    }
    public async Task<ICollection<CharacterEntity>> List()
    {
        return await _characterRepo.ListAsync();
    }
    public async Task<CharacterEntity> GetById(Guid characterId)
    {
        return await _characterRepo.FoundOrThrowAsync(characterId, Constants.Entities.CHARACTER + Constants.Errors.NOT_EXIST_ERROR);

    }
    public async Task<ICollection<CharacterEntity>> ListCharByUserId(Guid id)
    {
        return await _characterRepo.WhereAsync(c => c.UserId == id);
    }

    public async Task<ICollection<CharacterEntity>> ListCharByGameId(Guid id)
    {
        var gameServerIds = (await _gameServerService.ListServersByGameId(id)).Select(gs => gs.Id);
        var characterTypeIds = (await _characterTypeService.ListCharTypesByGameId(id)).Select(ct => ct.Id);
        var userIds = (await _gameUserService.ListUsersByGameId(id)).Select(u => u.Id);
        return await _characterRepo.WhereAsync(c => gameServerIds.Contains(c.GameServerId) 
                    || characterTypeIds.Contains(c.CharacterTypeId));
    }
    public async Task Create(CharacterEntity character)
    {
        await _characterRepo.CreateAsync(character);
    }
    public async Task Update(CharacterEntity character)
    {
        await CheckForDuplicateCharacter(character);
        await _characterRepo.UpdateAsync(character);
    }
    public async Task Delete(Guid characterId)
    {
        await _characterRepo.DeleteSoftAsync(characterId);
    }
    public async Task CheckForDuplicateCharacter(CharacterEntity character)
    {
        var cCheck = await _characterRepo.FirstOrDefaultAsync(
            c => c.UserId.Equals(character.UserId) 
            && c.CharacterTypeId.Equals(character.CharacterTypeId)
            && c.GameServerId.Equals(character.GameServerId));
        if (cCheck is not null)
        {
            if (character.Id == Guid.Empty || character.Id != cCheck.Id)
            {
                throw new BadRequestException(Constants.Entities.CHARACTER + Constants.Errors.ALREADY_EXIST_ERROR);
            }
        }
    }
}