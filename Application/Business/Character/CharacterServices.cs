using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterServices : ICharacterServices
{
    public readonly IGenericRepository<CharacterEntity> _characterRepo;

    public CharacterServices(IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterRepo = characterRepo;
    }
    public async Task<ICollection<CharacterEntity>> List()
    {
        return await _characterRepo.ListAsync();
    }
    public async Task<CharacterEntity> GetById(Guid characterId)
    {
        return await _characterRepo.FindByIdAsync(characterId);
    }
    public async Task<ICollection<CharacterEntity>> GetByUserId(Guid id)
    { 
        return await _characterRepo.WhereAsync(c=>c.UserId == id);
    }
    public async Task<ICollection<CharacterEntity>> GetByCharacterTypeId(Guid id)
    {
        return await _characterRepo.WhereAsync(c => c.CharacterTypeId == id);
    }
    public async Task<ICollection<CharacterEntity>> GetByGameServerId(Guid id)
    {
        return await _characterRepo.WhereAsync(c => c.GameServerId == id);
    }
    public async Task<int> Count()
    {
        return await _characterRepo.CountAsync();
    }
    public async Task Create(CharacterEntity character)
    {
        await CheckForDuplicateCharacter(character);
        await _characterRepo.CreateAsync(character);
    }
    public async Task Update(Guid characterId, CharacterEntity character)
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
            c => c.UserId.Equals(character.UserId) && c.GameServerId.Equals(character.GameServerId));
        if(cCheck is not null)
        {
            if(character.Id == Guid.Empty || character.Id != cCheck.Id)
            {
                throw new BadRequestException("The game already has this level's name");
            }
        }
    }
}
