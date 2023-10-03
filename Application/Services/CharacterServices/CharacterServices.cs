using Application.Interfaces;
using Domain.Entities.Character;
using System;
using System.Collections.ObjectModel;

namespace Application.Services.CharacterServices;

public class CharacterServices : ICharacterServices
{
    public readonly IGenericRepository<CharacterEntity> _characterRepo;

    public CharacterServices(IGenericRepository<CharacterEntity> characterRepo)
    {
        _characterRepo = characterRepo;
    }
    public async Task<ICollection<CharacterEntity>> List()
    {
        var cha = await _characterRepo.ListAsync();
        return cha;
    }
    public async Task<CharacterEntity> GetById(Guid characterId)
    {
        var cha = await _characterRepo.FindByIdAsync(characterId);
        if (cha == null)
        {
            throw new Exception($"Character not exist");
        }
        else
        {
            return cha;
        }
    }
    public async Task<ICollection<CharacterEntity>> GetById(Guid id, int typeId)
    { // TypeId: 1: UserId, 2: CharacterTypeId, 3: GameServerId
        ICollection<CharacterEntity> cha = new Collection<CharacterEntity>();
        if (typeId == 1)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.UserId.Equals(id));
        }
        else if (typeId == 2)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.CharacterTypeId.Equals(id));
        }
        else if (typeId == 3)
        {
            cha = await _characterRepo.WhereAsync(
                c => c.GameServerId.Equals(id));
        }
        //Return if exist
        if (cha.Count == 0)
        {
            throw new Exception($"Character or User/ Character Type/ Game Server not found");
        }
        else
        {
            return cha;
        }
    }
    public async Task<int> Count()
    {
        return await _characterRepo.CountAsync();
    }
    public async Task Create(CharacterEntity character)
    {
    }
    public async Task Update(Guid characterId, CharacterEntity character)
    {

    }
    public async Task Delete(Guid characterId)
    {

    }
}
